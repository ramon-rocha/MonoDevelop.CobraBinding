Cobra Language Binding for MonoDevelop
======================================
This is an addin for MonoDevelop that allows you to write, run, and debug 
programs written in the Cobra programming language.

http://cobra-language.com/

It currently supports the following features:

* Syntax Highlighting

* Underlining Syntax and Other Errors

* Folding Regions

* Multi-file Projects

* Compilation and Execution

* Interactive Debugging

* Limited Code and Parameter Completion

* Mouseover Tooltips


Compiling and Installing
========================
This addin is primarily developed on Ubuntu 13.10 using MonoDevelop.

However, it will work with Xamarin Studio on OS X using Mono or on Windows 
using either the .NET Framework or Mono.

Minimum Requirements
--------------------
* .NET Framework 4.0 or Mono 3.2.1

* MonoDevelop or Xamarin Studio 4.2

* Cobra 0.9.6

Installing the Precompiled Package
----------------------------------
A MonoDevelop addin repository is available at http://mdrepo.ramonrocha.com

- From the Tools > Add-in Manager window, select the Gallery tab.

- Select "Manage Repositories..." from the dropdown menu.

- Click Add and then enter the repository Url.

- Under "Language bindings", select "Cobra Language Binding" and click Install.

- Restart MonoDevelop to ensure all dependencies are correctly loaded.

Installing from Source
----------------------
An installation program that builds and installs the addin from source is
provided for convenience. Just execute...

    cobra install.cobra

...to compile and execute the installation program.

If you just want to compile the addin and not install it, you can execute:

    cobra install.cobra -run-args compile

If you are running the latest version of Cobra from subversion and you find
that the installer from the 'master' branch doesn't work, try to use the
'cobra-svn' branch instead.  If that still doesn't work, please submit a bug
report.

On Windows 64-bit with a 32-bit installation of Xamarin Studio, you'll need 
to make sure you've installed Cobra using the '-x86' installer option first.  
See below for more details.

Additional Requirements for Windows 64-bit
--------------------------------------------
Just skip this whole section if you are not running 64-bit Windows.

This addin references the MonoDevelop.Core.dll assembly.  The Cobra compiler 
gets the public types provided by an assembly using Assembly.GetExportedTypes 
via reflection.  If any of the dependencies of the loaded assembly are compiled 
for 32-bit only then an exception will be thrown.  This prevents compilation of 
the addin.  In this case, the Cobra compiler reports that the assembly 
MonoDevelop.Projects.Formats.MSBuild could not be found.  This is not specific 
to the Cobra compiler as the same reflection mechanism will throw the same 
exception in C#.

At this time, the workaround is to target the x86 platform when installing 
Cobra.  If you have already installed Cobra, you'll need to run the installer
again.  You do not need to uninstall the previously installed version.

Make sure to run these commands from a Visual Studio or Windows SDK Command
Prompt with the correct privileges (i.e. 'Run as Administrator').

Run the Cobra installer including the '-x86' option:

    cd\<path\to\cobra\workspace>\Source
    bin\install-from-workspace.bat -x86

You can verify success by executing:

    gacutil /l Cobra.Core

Check the reported processorArchitecure. There should be an entry that reads
'x86' instead of 'MSIL'.  It's okay to have multiple entries as long as at
least one them is 'x86'.  Now, just make sure to target your Cobra programs
to the x86 platform (the default option in MonoDevelop) when compiling,
running, and debugging, and you shouldn't have any issues.  If you are trying
to compile Cobra code from the command line, add the '-clr-platform:x86' option.


Status of Code Completion
=========================
Code completion is currently supported in a limited number of scenarios.
See below for specifics.

What Works?
-----------
 * Keyword completion (oh boy)
 * Namespaces and types from referenced assemblies and packages
 * Leading .dot and _underscore _completion
 * Local variable completion
 * Arbitrary.member.completion
 * Parameter completion

Isn't that everything?  Not quite.  The biggest problem is that most of the
"working" scenarios only work when there are no parsing errors in the file
(i.e. no red squiggly lines).  A caching mechanism works around this to
mitigate the annoyance but it's less than ideal.

What Doesn't Work?
------------------
 * Completion of identifiers for declarations made while parsing errors exist
 * Project references don't work quite right
 * ```if inherits``` and ```if implements``` blocks do not show all available members

In technical terms, because the addin is using the Cobra parser from
Cobra.Compiler.dll to generate the abstract syntax tree (AST) for the file
being edited, any parsing errors will cause the parser to throw a
SourceException which prevents the AST from being created. This in turn
prevents the addin from knowing which document regions and text identifiers
correspond to which nodes in the AST.

In layman's terms, the addin doesn't know how to get information useful for
completion from Cobra source code that contains errors.  So, if you're in a
class region and type:

    def printTotal(first as int, second as int)
        print

This is a valid method.  As written it will print a blank line.  Now, when you
press space and then ```f``` You will get a completion proposal for ```first```
among other options.

But in the next example, completion doesn't quite work the way we want.

    def printNums(numbers as List<of int>)
        for n in 

When you type ```n``` again, you might expect to see a completion option for
```numbers``` and another for ```n```.  The problem is that this is not a valid
method yet.  The ```for``` loop is unfinished so the addin cannot get valid
information from the Cobra parser about this method yet.

What's the Plan?
----------------
While not perfect, the way completion works in the addin right now is still
useful. To address the shortcomings with the current implementation of code
completion, a new source analysis library will be developed.  This library will
include a Cobra parser and other mechanisms that can better understand Cobra
source code that contain errors and still provide useful information.

That is the long-term goal but this is still in the early planning phases.
In the near-term, we can investigate tweaking the stock CobraParser or possibly
inheriting from it and overriding its behaviors to get better data for completion:

http://cobra-language.com/trac/cobra/browser/cobra/trunk/Source/CobraParser.cobra


Contributing
============
Any and all help enhancing the addin is appreciated.  See below for tips on getting started.

Interactive Debugging
---------------------
This is not how I typically troubleshoot or test the addin but it should be
technically possible. You can check here for more information on how to do this:

http://stackoverflow.com/questions/9637994/how-can-i-debug-monodevelop-add-ins-with-monodevelop

"Debugging" via the Console
---------------------------
This is usually how I go about debugging the addin.  You can launch MonoDevelop
from the command line with --no-redirect which will allow you to see all 'print'
and 'trace' statements.

On Linux:

    monodevelop --no-redirect

On Mac:

    cd /Applications/Xamarin\ Studio.app/Contents/MacOS/
    ./XamarinStudio --no-redirect

On Windows, you cannot use the --no-redirect option.  Instead review the log files located at:

    C:\Users\<username>\AppData\Local\XamarinStudio-4.0\Logs

Debugging Tips
--------------
When manually testing your changes to the addin, it is recommended that you
modify install.cobra so that the addin is compiled and installed with contracts
and assertions enabled.  To do this, search install.cobra for this line:

    cobraArgs.append(' -turbo:yes')

and change it to this:

    cobraArgs.append(' -include-tests:no')

This will include contracts and asserts but not the unit tests.  It is
important that tests not be included in the compiled version of the addin as
performance is significantly degraded otherwise. You shouldn't include
this modification in your pull request though.  Enabling contracts in a
"production" build would also negatively impact performance.

Running Tests
-------------
A script to run the unit tests is located here:

    <solutionFolder>/CobraBinding/scripts/test

The script file has only been tested in Ubuntu.  On Windows, use test.bat instead.
There is a lot of code that is not covered by these tests.  For those scenarios,
check this folder which contains a set of text files describing manual tests that
can be performed to check for regressions:

    <solutionFolder>/CobraBinding/manual_tests

You are not required to run through these manual tests when making changes.

Sending Patches/Making Pull-Requests
------------------------------------
Right now, there is no formal policy for this.  Submit whatever you write
however you want and I'll do my best to make sense of it.  I will test it but
please make sure you did not break any of the existing automated tests (see
above).

Low-hanging Fruit
-----------------
Try one of these tasks.  These are things that are doable by one motivated individual.
Be sure to check the end of this document for useful links to MonoDevelop addin development
in general.

### Try the addin and provide feedback
If something doesn't work the way you expect or you have a question/enhancement request,
just submit a new issue here.

https://github.com/ramon-rocha/MonoDevelop.CobraBinding/issues

You don't have to worry about tagging it correctly.

### Create icons for project and file templates
Right now, the default generic icons are being used.  Eventually, we'd like to get some custom icons.
The icons for projects and files are specified in the individual XML template files (see next task).

### Create additional project or file templates
You can use the existing templates as a starting point for new ones.  They are located here:

    <solutionFolder>/CobraBinding/templates
 
You'll also need to update install.cobra and then reinstall the addin to see the results of your changes.

Larger Todo Tasks
-----------------
These will require a bit more effort and coordination with other feature
development...

* Implement a Code Formatter for smarter indentation

* Implement an Ambience class for document outline support

* Fix bugs with code completion

Cobra Forums Discussion
-----------------------
This section will be a collection of links to relevant posts on the Cobra forums.

http://cobra-language.com/forums/viewtopic.php?f=4&t=1047 


Relevant Documentation
----------------------
These are some links to available documentation for adding support for a new language to MonoDevelop.

http://monodevelop.com/Developers/Articles/Language_Addins

http://monodevelop.com/Developers/Articles/Syntax_Mode_Definition

http://monodevelop.com/Developers/Articles/API_Overview

http://monodevelop.com/Developers/Articles/Creating_a_Simple_Add-in


Reference Implementations for other Languages
---------------------------------------------
These are examples of existing language bindings for MonoDevelop.

https://github.com/mono/monodevelop/tree/master/main/src/addins/CSharpBinding

https://github.com/aBothe/Mono-D/tree/master/MonoDevelop.DBinding

https://github.com/fsharp/fsharpbinding

https://github.com/mono/monodevelop/tree/master/extras/BooBinding

