using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.TextEditor;
using ZetaColorEditor.Runtime;

namespace ClarionEdge.InsertClarionColor
{
    public class ShowColorDialog : AbstractMenuCommand
    {
        public override void Run()
        {
            using (ColorEditorForm cd = new ColorEditorForm())
            {
                ColorConverter cc = new ColorConverter();
                cd.SelectedColor = (Color)cc.ConvertFromString(PropertyService.Get("ClarionEdge.InsertClarionColor.LastSelectedColor", ""));
                //cd.ExternalColorEditorInformationProvider = new ClarionColorProvider();
                if (cd.ShowDialog(ICSharpCode.SharpDevelop.Gui.WorkbenchSingleton.MainForm) == DialogResult.OK)
                {
                    //string ext = Path.GetExtension(textarea.FileName).ToLowerInvariant();
                    string colorstr = string.Empty;
                    if (cd.SystemColorsTabPageSelected == true)
                        colorstr = GetClarionSystemColorEquate(cd.SelectedColor);
                    if (colorstr == string.Empty)
                        colorstr = GetClarionColorEquate(cd.SelectedColor);

                    PropertyService.Set("ClarionEdge.InsertClarionColor.LastSelectedColor", cc.ConvertToString(cd.SelectedColor));

                    IWorkbenchWindow window = WorkbenchSingleton.Workbench.ActiveWorkbenchWindow;
                    if (window == null || 
                        !(window.ViewContent is ITextEditorControlProvider) || 
                        window.ActiveViewContent is SoftVelocity.Generator.UI.ApplicationMainWindowControl_ViewContent ||
                        window.ActiveViewContent is SoftVelocity.Generator.Editor.CommonClarionGenDesignerView)
                    {
                        if (MessageService.AskQuestion("No active editor window found, copy selected color (" + colorstr + ") to the clipboard?"))
                            Clipboard.SetText(colorstr);

                        // This also works except it is not much use because all the dialogs it would be handy in are modal
                        // Oh and the property grid loses focus so it doesn't work there either.
                        //SendKeys.Send(colorstr);
                    }
                    else
                    {
                        TextEditorControl textarea = ((ITextEditorControlProvider)window.ViewContent).TextEditorControl;
                        textarea.Document.Insert(textarea.ActiveTextAreaControl.Caret.Offset, colorstr);
                        int lineNumber = textarea.Document.GetLineNumberForOffset(textarea.ActiveTextAreaControl.Caret.Offset);
                        textarea.ActiveTextAreaControl.Caret.Column += colorstr.Length;
                        textarea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, new TextLocation(0, lineNumber)));
                        textarea.Document.CommitUpdate();
                    }
                }
            }
        }

        private string GetClarionColorEquate(Color color)
        {
            if (color == Color.Black)
                return "COLOR:Black";
            if (color == Color.Maroon)
                return "COLOR:Maroon";
            if (color == Color.Green)
                return "COLOR:Green";
            if (color == Color.Olive)
                return "COLOR:Olive";
            if (color == Color.Orange)
                return "COLOR:Orange";
            if (color == Color.Navy)
                return "COLOR:Navy";
            if (color == Color.Purple)
                return "COLOR:Purple";
            if (color == Color.Teal)
                return "COLOR:Teal";
            if (color == Color.Gray)
                return "COLOR:Gray";
            if (color == Color.Silver)
                return "COLOR:Silver";
            if (color == Color.Red)
                return "COLOR:Red";
            if (color == Color.Lime)
                return "COLOR:Lime";
            if (color == Color.Yellow)
                return "COLOR:Yellow";
            if (color == Color.Blue)
                return "COLOR:Blue";
            if (color == Color.Fuchsia)
                return "COLOR:Fuchsia";
            if (color == Color.Aqua)
                return "COLOR:Aqua";
            if (color == Color.White)
                return "COLOR:White";

            return string.Format("00{0:X2}{1:X2}{2:X2}h", color.B, color.G, color.R);
        }

        private string GetClarionSystemColorEquate(Color color)
        {
            // try to convert the selection to one of the clarion system color equates.
            if (color == SystemColors.ScrollBar)
                return "COLOR:SCROLLBAR";
            //else if (color == SystemColors.b COLOR:BACKGROUND        EQUATE (80000001H)
            else if (color == SystemColors.ActiveCaption)
                return "COLOR:ACTIVECAPTION";
            else if (color == SystemColors.InactiveCaption)
                return "COLOR:INACTIVECAPTION";
            else if (color == SystemColors.Menu)
                return "COLOR:MENU";
            else if (color == SystemColors.MenuBar)
                return "COLOR:MENUBAR";
            else if (color == SystemColors.Window)
                return "COLOR:WINDOW";
            else if (color == SystemColors.WindowFrame)
                return "COLOR:WINDOWFRAME";
            else if (color == SystemColors.MenuText)
                return "COLOR:MENUTEXT";
            else if (color == SystemColors.WindowText)
                return "COLOR:WINDOWTEXT";
            else if (color == SystemColors.ActiveCaptionText)
                return "COLOR:CAPTIONTEXT";
            else if (color == SystemColors.ActiveBorder)
                return "COLOR:ACTIVEBORDER";
            else if (color == SystemColors.InactiveBorder)
                return "COLOR:INACTIVEBORDER";
            else if (color == SystemColors.AppWorkspace)
                return "COLOR:APPWORKSPACE";
            else if (color == SystemColors.Highlight)
                return "COLOR:HIGHLIGHT";
            else if (color == SystemColors.HighlightText)
                return "COLOR:HIGHLIGHTTEXT";
            else if (color == SystemColors.ButtonFace)
                return "COLOR:BTNFACE";
            else if (color == SystemColors.ButtonShadow)
                return "COLOR:BTNSHADOW";
            else if (color == SystemColors.GrayText)
                return "COLOR:GRAYTEXT";
            else if (color == SystemColors.ControlText)
                return "COLOR:BTNTEXT";
            else if (color == SystemColors.InactiveCaptionText)
                return "COLOR:INACTIVECAPTIONTEXT";
            else if (color == SystemColors.ButtonHighlight)
                return "COLOR:BTNHIGHLIGHT";
            else
                return string.Empty;
        }
    }

}
