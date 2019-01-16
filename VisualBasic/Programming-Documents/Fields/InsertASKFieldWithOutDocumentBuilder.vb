﻿Imports Microsoft.VisualBasic
Imports System.IO
Imports Aspose.Words
Imports Aspose.Words.Fields
Public Class InsertASKFieldWithOutDocumentBuilder
    Public Shared Sub Run()
        ' ExStart:InsertASKFieldWithOutDocumentBuilder
        ' The path to the documents directory.
        Dim dataDir As String = RunExamples.GetDataDir_WorkingWithFields()
        Dim doc As New Document(dataDir & Convert.ToString("in.doc"))
        ' Get paragraph you want to append this Ask field to
        Dim para As Paragraph = DirectCast(doc.GetChildNodes(NodeType.Paragraph, True)(1), Paragraph)

        ' We want to insert an Ask field like this:
        ' { ASK \"Test 1\" Test2 \\d Test3 \\o }

        ' Create instance of FieldAsk class and lets build the above field code
        Dim field As FieldAsk = DirectCast(para.AppendField(FieldType.FieldAsk, False), FieldAsk)

        ' { ASK \"Test 1\" " }
        field.BookmarkName = "Test 1"

        ' { ASK \"Test 1\" Test2 }
        field.PromptText = "Test2"

        ' { ASK \"Test 1\" Test2 \\d Test3 }
        field.DefaultResponse = "Test3"

        ' { ASK \"Test 1\" Test2 \\d Test3 \\o }
        field.PromptOnceOnMailMerge = True

        ' Finally update this Ask field
        field.Update()

        dataDir = dataDir & Convert.ToString("InsertASKFieldWithOutDocumentBuilder_out.doc")
        doc.Save(dataDir)

        ' ExEnd:InsertASKFieldWithOutDocumentBuilder
        Console.WriteLine(Convert.ToString(vbLf & "ASK field without using document builder inserted successfully." & vbLf & "File saved at ") & dataDir)
    End Sub
End Class
