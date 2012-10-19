Cobra Language Binding for MonoDevelop
======================================
This is an addin for MonoDevelop that allows you to write, run, and debug 
programs written in the Cobra programming language.

http://cobra-language.com/

It currently supports the following features:

* Syntax Highlighting

* Folding Regions

* Multi-file Projects

* Compilation and Execution

* Debugging


Compiling and Installing
========================
This addin is primarily developed on Ubuntu 12.04 LTS using MonoDevelop 3.0 
from this PPA: 

https://launchpad.net/~keks9n/+archive/monodevelop-latest

However, it will work on OS X using Mono or on Windows using either the .NET 
Framework or Mono. An installation program is provided for convenience. Just 
execute...

    cobra install.cobra

...to compile and execute the installation program.

On Windows 7 64-bit with a 32-bit installation of MonoDevelop, you'll need 
to make sure you've installed Cobra using the '-x86' installer option first.  
See below for more details.

Requirements
------------
* .NET Framework 4 or Mono 2.10

* MonoDevelop 3.0

* Cobra 0.9-svn-2817

Additional Requirements for Windows 7 64-bit
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
Cobra.  If you have already installed Cobra, you'll need to reinstall it.  

Make sure to run these commands from the Visual Studio or Windows SDK Command 
Prompt with the correct privileges (i.e. 'Run as Administrator').

First, remove Cobra.Core from the GAC:

    gacutil /u Cobra.Core

Next, set your system to use the 32-bit CLR by executing this command:

    C:\Windows\Microsoft.NET\Framework64\v2.0.50727\Ldr64.exe setwow

Then, run the Cobra installer again this time including the '-x86' option:

    cd\<path\to\cobra\workspace>\Source
    bin\install-from-workspace.bat -x86

Finally, restore your system to defaulting to the 64-bit CLR via:

    C:\Windows\Microsoft.NET\Framework64\v2.0.50727\Ldr64.exe set64

You can verify success by executing:

    gacutil /l Cobra.Core

Check the reported processorArchitecure. It should read 'x86' and not 
'MSIL'.  Now, as long you make sure to target your Cobra programs to the x86 
platform (the default option in MonoDevelop) when compiling, running, and 
debugging, you shouldn't have any issues.  If you have .NET 4.5 installed, 
you need to make sure you set .NET as the active runtime when debugging as not 
all of 4.5 mscorlib has been implemented in Mono yet.

NOTE: Some alternate workarounds may include compiling MonoDevelop from source 
or maybe installing Cobra targetting the 'anycpu32bitpreferred' platform 
on .NET 4.5 instead.  These alternatives have not been tried or tested yet.


Installation
------------
At this time, the easiest way to install the addin is to use the provided 
installation program.  Close MonoDevelop if it's open and then on Ubuntu, 
Windows 32-bit, or Mac execute:

    cobra install.cobra

Then, start MonoDevelop and create a new solution.  You should see a 'Cobra' 
section with various Cobra project templates.

On Windows 64-bit, make sure to target the x86 platform:

    cobra -clr-platform:x86 install.cobra

If you just want to compile the addin and not install it, you can execute:

    cobra install.cobra -run-args compile

Alternatively, on Ubuntu, you can use xbuild and a bash script instead:

    cd Gui
    xbuild
    cd ../CobraBinding
    ./scripts/build


Contributing
============
Any and all help is appreciated.  See below for tips on getting started.


Low-hanging Fruit
-----------------
Try one of these tasks...

* Try the addin and report issues

* Improve the installer and/or package the addin (man mdtool)

* Create icons for project and file templates

* Create additional project or file templates

* Create syntax highlighting color schemes that match those on the Cobra website

* Add support for MSBuild Items


Larger Todo Tasks
-----------------
These will require a bit more effort...

* Implement the Type System Parser

* Implement the Code Completion extension

* Implement a Code Formatter for smarter indentation

* Implement an Ambience class for tool tips and document outline support

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

https://github.com/fsharp/fsharpbinding

https://github.com/aBothe/Mono-D/tree/master/MonoDevelop.DBinding

https://github.com/mono/monodevelop/tree/master/extras/BooBinding
