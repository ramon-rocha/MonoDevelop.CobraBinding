cd /d %~dp0

cd refs

cobra -compile -v -timeit -target:lib -out:"..\\..\\bin\\Debug\\MonoDevelop.CobraBinding.dll" ^
-namespace:MonoDevelop.CobraBinding ^
-contracts:inline -include-asserts:no -include-nil-checks:no ^
-include-tests:no -include-traces:yes -optimize ^
-color:yes -debug:full ^
-sharp-args:"-res:\"..\\..\\CobraBinding.addin.xml\" -res:\"..\\..\\CobraSyntaxMode.xml\" -res:\"..\\..\\templates/ConsoleProject.xpt.xml\" -res:\"..\\..\\templates/EmptyCobraFile.xft.xml\" -res:\"..\\..\\templates/EmptyProject.xpt.xml\" -res:\"..\\..\\templates/GtkSharp2Project.xpt.xml\" -res:\"..\\..\\templates/LibraryProject.xpt.xml\"" ^
-ref:atk-sharp.dll ^
-ref:gdk-sharp.dll ^
-ref:glib-sharp.dll ^
-ref:gtk-sharp.dll ^
-ref:ICSharpCode.NRefactory.CSharp.dll ^
-ref:ICSharpCode.NRefactory.dll ^
-ref:Mono.Cecil.dll ^
-ref:Mono.TextEditor.dll ^
-ref:MonoDevelop.CobraBinding.Gui.dll ^
-ref:MonoDevelop.Core.dll ^
-ref:MonoDevelop.Ide.dll ^
-ref:MonoDevelop.Projects.Formats.MSBuild.exe ^
-ref:pango-sharp.dll ^
-ref:System.Core ^
..\..\CobraLanguageBinding.cobra ^
..\..\CacheManager.cobra ^
..\..\CompilerManager.cobra ^
..\..\Extensions.cobra ^
..\..\Project\CobraCompilerParameters.cobra ^
..\..\Project\CobraProjectParameters.cobra ^
..\..\Project\CompilerBuildOptionsPanel.cobra ^
..\..\Project\GeneralBuildOptionsPanel.cobra ^
..\..\TypeSystem\CobraModuleVisitor.cobra ^
..\..\TypeSystem\LineInformation.cobra ^
..\..\TypeSystem\ParsedCobraDocument.cobra ^
..\..\TypeSystem\Parser.cobra ^
..\..\Completion\BoxMembersList.cobra ^
..\..\Completion\CobraCompletionList.cobra ^
..\..\Completion\CobraCompletionTextEditorExtension.cobra ^
..\..\Completion\CobraParameterDataProvider.cobra ^
..\..\Completion\DataFactory.cobra ^
..\..\Completion\Icons.cobra ^
..\..\Completion\MethodVarsList.cobra ^
..\..\Completion\NamedNodeData.cobra ^
..\..\Completion\KeywordCompletionData.cobra ^
..\..\Completion\NameSpaceDeclsList.cobra ^
..\..\Tooltips\Extensions.cobra ^
..\..\Tooltips\TooltipProvider.cobra ^
..\..\Tooltips\KeywordData.cobra ^
..\..\Commands\CommandId.cobra ^
..\..\Commands\GoToDeclarationHandler.cobra
