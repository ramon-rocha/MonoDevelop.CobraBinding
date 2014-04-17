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

* Cobra svn:3017 (post 0.9.6)

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

On Windows 64-bit with a 32-bit installation of Xamarin Studio, you'll need 
to make sure you've installed Cobra using the '-x86' installer option first.  
See below for more details.

Additional Requirements for Windows 64-bit
--------------------------------------------
Just ignore this whole section if you are not running 64-bit Windows.

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


Getting Help
============
You can submit bug reports or enhancement requests on the project's issues
list at GitHub.

https://github.com/ramon-rocha/MonoDevelop.CobraBinding/issues

You can also get help on the Cobra discussion forums.

http://cobra-language.com/forums/viewforum.php?f=4