Cobra Language Binding for MonoDevelop
======================================
This is an addin for MonoDevelop that allows you to write, run, and debug programs written in the Cobra programming language.
http://cobra-language.com/

This is the 'cobra' branch of the addin and is a work in progress.

At present, this addin only works on the 3.0 series of MonoDevelop.


To Compile and Use
------------------

This addin is mostly developed on Ubuntu 12.04 LTS using MonoDevelop 3.0 from this PPA: https://launchpad.net/~keks9n/+archive/monodevelop-latest
I haven't been able to compile successfully on Windows yet so these instructions apply to Ubuntu.

1) Open the solution file in MonoDevelop 3.X

2) Build the 'Gui' project and then exit MonoDevelop. This will generate MonoDevelop.CobraBinding.Gui.dll in the Gui/bin/Debug folder.  Do not attempt to build the 'CobraBinding' project since it's written in Cobra and you need the addin to compile Cobra code from MonoDevelop.  The 'Gui' project is written in C# to utilize the existing design tools.

3) Next, from the command line, change into the CobraBinding directory and execute ./scripts/build.  This will generate an assembly called MonoDevelop.CobraBinding.dll in the CobraBinding/bin/Debug folder.

4) Copy both generated assemblies to the MonoDevelop addins folder. This location depends on your operating system.

- Ubuntu : ~/.local/share/MonoDevelop-3.0/LocalInstall/Addins

- Mac : ~/Library/Application Support/MonoDevelop-3.0/LocalInstall/Addins

- Windows 7 : ~/AppData/Local/MonoDevelop-3.0/LocalInstall/Addins (on Windows ~ is usually c:\users\<username>, also note that AppData is a hidden folder)

If any folders do not exist, you should create them manually.

5) Restart MonoDevelop and you should now have a "Cobra" section when creating a new project/solution.


Contributing
============
Any and all help is appreciated.  See below for tips on getting started.


Low-hanging Fruit
-----------------
These are some of the easier tasks that still need to be done...

* Create icons for project and file templates

* Create additional file templates

* Create some color schemes that match those on the Cobra website.


Larger Todo Tasks
-----------------
These will require a bit more effort...

* Proper handling for project options and compiler configuration. (in progress)

* Folding Parser

* Type System Parser

* Code Completion

* Code Formatter

* Ambience class for tool tips

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
