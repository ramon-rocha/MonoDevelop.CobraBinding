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
This addin is primarily developed on Ubuntu 14.04 LTS using MonoDevelop.

However, it will work with Xamarin Studio on OS X using Mono or on Windows 
using either the .NET Framework or Mono.

Minimum Requirements
--------------------
* .NET Framework 4.5 or Mono 3.2.1

* MonoDevelop or Xamarin Studio 5.0

* Cobra svn:3115 (post 0.9.6)

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


Getting Help
============
You can submit bug reports or enhancement requests on the project's issues
list at GitHub.

https://github.com/ramon-rocha/MonoDevelop.CobraBinding/issues

You can also get help on the Cobra discussion forums.

http://cobra-language.com/forums/viewforum.php?f=4
