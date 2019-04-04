using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System.Windows.Controls;

namespace HIPSClient.HipsTinkerTool.AvalonEditSupport
{
  public static class ExtentionAvalonEdit
  {
    public static void AvalonEditContextMenu(this TextEditor value)
    {
      var oContextMenu = new ContextMenu();
      oContextMenu.Height = 80;
      oContextMenu.Width = 190;

      MenuItem Copy = new MenuItem();
      Copy.Command = System.Windows.Input.ApplicationCommands.Copy;
      Copy.Header = "_Copy";
      oContextMenu.Items.Add(Copy);

      MenuItem Paste = new MenuItem();
      Paste.Command = System.Windows.Input.ApplicationCommands.Paste;
      Paste.Header = "_Paste";
      oContextMenu.Items.Add(Paste);

      MenuItem SelectAll = new MenuItem();
      SelectAll.Command = System.Windows.Input.ApplicationCommands.SelectAll;
      SelectAll.Header = "_SelectAll";
      oContextMenu.Items.Add(SelectAll);

      value.ContextMenu = oContextMenu;
    }

    public static void SetSyntaxType(this TextEditor value, AvalonEditSupport.AvalonEditSyntaxHighlightingTypes Syntaxtype)
    {
      switch (Syntaxtype)
      {
        case AvalonEditSupport.AvalonEditSyntaxHighlightingTypes.HL7V2:
          value.SyntaxHighlighting = AvalonEditSupport.ExtentionAvalonEdit.AvalonEditSyntaxHighlighting(@"C:\temp\AvalonEdit\HL7Syntax.xshd");
          break;
        case AvalonEditSupport.AvalonEditSyntaxHighlightingTypes.XML:
          {
            var typeConverter = new ICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter();
            value.SyntaxHighlighting = (ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition)typeConverter.ConvertFrom("XML");
            break;
          }
        case AvalonEditSupport.AvalonEditSyntaxHighlightingTypes.HTML:
          {
            var typeConverter = new ICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter();
            value.SyntaxHighlighting = (ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition)typeConverter.ConvertFrom("HTML");
            break;
          }
        default:
          throw new NotImplementedException(String.Format("AvalonEditSyntaxHighlightingTypes of '{0}' has not been implemneted in the tool as yet.", Syntaxtype.ToString()));
      }
    }

    public static ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition AvalonEditSyntaxHighlighting(string value)
    {
      using (System.IO.Stream s = System.IO.File.OpenRead(value))
      {
        using (System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(s))
        {
          return ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load
              (reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
        }
      }
    }



    public static string GetWordUnderMouse(this TextDocument document, TextViewPosition position)
    {
      string wordHovered = string.Empty;

      var line = position.Line;
      var column = position.Column;

      var offset = document.GetOffset(line, column);
      if (offset >= document.TextLength)
        offset--;

      var textAtOffset = document.GetText(offset, 1);

      // Get text backward of the mouse position, until the first space
      while (!string.IsNullOrWhiteSpace(textAtOffset))
      {
        wordHovered = textAtOffset + wordHovered;

        offset--;

        if (offset < 0)
          break;

        textAtOffset = document.GetText(offset, 1);
      }

      // Get text forward the mouse position, until the first space
      offset = document.GetOffset(line, column);
      if (offset < document.TextLength - 1)
      {
        offset++;

        textAtOffset = document.GetText(offset, 1);

        while (!string.IsNullOrWhiteSpace(textAtOffset))
        {
          wordHovered = wordHovered + textAtOffset;

          offset++;

          if (offset >= document.TextLength)
            break;

          textAtOffset = document.GetText(offset, 1);
        }
      }

      return wordHovered;
    }


    public static ToolTip GetHL7V2PostionDisplayInformation(this TextDocument document, TextViewPosition position, Dictionary<PeterPiper.Hl7.V2.Schema.Model.VersionsSupported, PeterPiper.Hl7.V2.Schema.Support.SchemaSupport> oSchemaSupportDic)
    {
      var oLine = document.GetLineByNumber(1);
      var LineText = document.GetText(oLine.Offset, oLine.Length);
            
      PeterPiper.Hl7.V2.Model.IMessage oMessageMSH = PeterPiper.Hl7.V2.Model.Creator.Message(LineText);            

      oLine = document.GetLineByNumber(position.Line);
      if (oLine.Length == 0)
        return null;
      if (position.Column > oLine.Length)
        return null;

      LineText = document.GetText(oLine.Offset, oLine.Length);
      var V2Detail = HL7V2Support.Hl7V2EditorSupport.GetPathDetails(LineText, position.Column, oMessageMSH, oSchemaSupportDic);     
      var toolTip = new ToolTip();
      toolTip.Content = HL7V2Support.Hl7V2EditorSupport.FormatToolTipText(V2Detail);      
      return toolTip;
    }


    public static string GetHL7V2PostionUnderMouse1(this TextDocument document, TextViewPosition position, PeterPiper.Hl7.V2.Model.IMessageDelimiters oMessageDelimiters)
    {
      string LineHovered = string.Empty;
      string ElementHovered = string.Empty;
      int ElementHoveredCharPostion = 0;
      int LineHoveredCharPostion = 0;

      var line = position.Line;
      var column = position.Column;

      var offset = document.GetOffset(line, column);
      if (offset >= document.TextLength)
        offset--;

      var textAtOffset = document.GetText(offset, 1);

      // Get text backward of the mouse position, until the first space
      bool FoundElementBeginning = false;
      while (textAtOffset != "\r")
      {
        LineHovered = textAtOffset + LineHovered;
        while (!FoundElementBeginning)
        {
          FoundElementBeginning = FindHL7FieldDelimeter(textAtOffset, oMessageDelimiters);
          break;
        }
        while (!FoundElementBeginning)
        {
          ElementHovered = textAtOffset + LineHovered;
          break;
        }
        offset--;

        if (offset < 0)
          break;

        textAtOffset = document.GetText(offset, 1);
      }

      LineHoveredCharPostion = LineHovered.Length; 
      ElementHoveredCharPostion = ElementHovered.Length;

      // Get text forward the mouse position, until the first space
      bool FoundElementEnd = false;
      offset = document.GetOffset(line, column);
      if (offset < document.TextLength - 1)
      {
        offset++;

        textAtOffset = document.GetText(offset, 1);
        while (!FindHL7FieldDelimeter(textAtOffset, oMessageDelimiters))
        {
          LineHoveredCharPostion++;
          break;
        }

        while (textAtOffset != "\r")
        {
          LineHovered = LineHovered + textAtOffset;

          while (!FoundElementEnd)
          {            
            FoundElementEnd = FindHL7FieldDelimeter(textAtOffset, oMessageDelimiters);
            break;
          }
          while (!FoundElementEnd)
          {
            ElementHovered = ElementHovered + textAtOffset;
            break;
          }




          offset++;

          if (offset >= document.TextLength)
            break;

          textAtOffset = document.GetText(offset, 1);
        }
      }
      
      LineHovered = LineHovered.Substring(0, LineHoveredCharPostion);

      return LineHovered;
    }

    private static bool FindHL7FieldDelimeter(string item, PeterPiper.Hl7.V2.Model.IMessageDelimiters oMessageDelimiters)
    {
      if (oMessageDelimiters.Field.ToString() == item)
        return true;
      else
        return false;
    }

    


    //Used for typing so that when you type a word and then a dot i.e "myClassInstance." this function returns "myClassInstance"
    //needs to run on a TextEntered event as example below:
    //void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
    //{
    //  if (e.Text == ".")
    //  {
    //    var previousWord = textEditor.GetWordBeforeDot();
    //  }
    //}
    public static string GetWordBeforeDot(this TextEditor textEditor)
    {
      var wordBeforeDot = string.Empty;

      var caretPosition = textEditor.CaretOffset - 2;

      var lineOffset = textEditor.Document.GetOffset(textEditor.Document.GetLocation(caretPosition));

      string text = textEditor.Document.GetText(lineOffset, 1);

      // Get text backward of the mouse position, until the first space
      while (!string.IsNullOrWhiteSpace(text) && text.CompareTo(".") > 0)
      {
        wordBeforeDot = text + wordBeforeDot;

        if (caretPosition == 0)
          break;

        lineOffset = textEditor.Document.GetOffset(textEditor.Document.GetLocation(--caretPosition));

        text = textEditor.Document.GetText(lineOffset, 1);
      }

      return wordBeforeDot;
    }


  }
}
