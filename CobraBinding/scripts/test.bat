cd /d %~dp0

cd refs

cobra -test -clr-platform:x86 -color:yes ^
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
..\..\CompilerManager.cobra ^
..\..\Project\CobraCompilerParameters.cobra ^
..\..\Project\CompilerBuildOptionsPanel.cobra ^
..\..\TypeSystem\CobraModuleVisitor.cobra ^
..\..\TypeSystem\LineInformation.cobra ^
..\..\TypeSystem\ParsedCobraDocument.cobra ^
..\..\TypeSystem\Parser.cobra ^
..\..\Completion\BoxMembersList.cobra ^
..\..\Completion\CobraCompletionList.cobra ^
..\..\Completion\CobraCompletionTextEditorExtension.cobra ^
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
