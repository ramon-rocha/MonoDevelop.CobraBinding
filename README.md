Cobra Language Binding for MonoDevelop
======================================
This is an addin for MonoDevelop that allows you to write, run, and debug programs written in the Cobra programming language.
http://cobra-language.com/

At present, this addin only works on the 2.8 series of MonoDevelop.


To Compile and Use
------------------

1) Open the solution file in MonoDevelop 2.8

2) Select Build > Build All.  This will generate a MonoDevelop.CobraBinding.dll assembly file in CobraBinding/bin/Debug.

3) Copy MonoDevelop.CobraBinding.dll to the MonoDevelop addins folder. This location depends on your operating system.

- Ubuntu : ~/.local/share/MonoDevelop-2.8/LocalInstall/Addins

- Mac : ~/Library/Application Support/MonoDevelop-2.8/LocalInstall/Addins

- Windows 7 : ~/AppData/Local/MonoDevelop-2.8/LocalInstall/Addins (on Windows ~ is usually c:\users\<username>, also note that AppData is a hidden folder)

If any folders do not exist, you should create them manually.

4) Restart MonoDevelop and you should now have a "Cobra" section when creating a new project/solution.


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

* Parser, Autocompletion, code formatting, code folding, etc.

* Rewrite it in Cobra! This one will actually be easy once the other tasks are done ;)


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

https://github.com/fsharp/fsharpbinding

https://github.com/mono/monodevelop/tree/master/main/src/addins/CSharpBinding

https://github.com/aBothe/Mono-D/tree/master/MonoDevelop.DBinding

https://github.com/mono/monodevelop/tree/master/extras/BooBinding
