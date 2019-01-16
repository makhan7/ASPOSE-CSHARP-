﻿
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Aspose.Words
Imports Aspose.Words.Reporting

Namespace LINQ
    Public Class HelloWorld
        Public Shared Sub Run()
            ' ExStart:HelloWorld
            ' The path to the documents directory.
            Dim dataDir As String = RunExamples.GetDataDir_LINQ()

            Dim fileName As String = "HelloWorld.doc"
            ' Load the template document.
            Dim doc As New Document(dataDir & fileName)

            ' Create an instance of sender class to set it' S properties.
            Dim sender As New Sender() With { _
                .Name = "LINQ Reporting Engine", _
                .Message = "Hello World" _
            }

            ' Create a Reporting Engine.
            Dim engine As New ReportingEngine()

            ' Execute the build report.
            engine.BuildReport(doc, sender, "sender")

            dataDir = dataDir & RunExamples.GetOutputFilePath(fileName)

            ' Save the finished document to disk.
            doc.Save(dataDir)
            ' ExEnd:HelloWorld
            Console.WriteLine(Convert.ToString(vbLf & "Template document is populated with the data about the sender." & vbLf & "File saved at ") & dataDir)

        End Sub
    End Class
End Namespace