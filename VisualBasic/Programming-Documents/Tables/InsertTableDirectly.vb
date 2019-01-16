﻿Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Drawing
Imports System.Diagnostics
Imports Aspose.Words
Imports Aspose.Words.Tables
Public Class InsertTableDirectly
    Public Shared Sub Run()
        ' ExStart:InsertTableDirectly
        ' The path to the documents directory.
        Dim dataDir As String = RunExamples.GetDataDir_WorkingWithTables()

        Dim doc As New Document()
        ' We start by creating the table object. Note how we must pass the document object
        ' To the constructor of each node. This is because every node we create must belong
        ' To some document.
        Dim table As New Table(doc)
        ' Add the table to the document.
        doc.FirstSection.Body.AppendChild(table)

        ' Here we could call EnsureMinimum to create the rows and cells for us. This method is used
        ' To ensure that the specified node is valid, in this case a valid table should have at least one
        ' Row and one cell, therefore this method creates them for us.

        ' Instead we will handle creating the row and table ourselves. This would be the best way to do this
        ' If we were creating a table inside an algorthim for example.
        Dim row As New Row(doc)
        row.RowFormat.AllowBreakAcrossPages = True
        table.AppendChild(row)

        ' We can now apply any auto fit settings.
        table.AutoFit(AutoFitBehavior.FixedColumnWidths)

        ' Create a cell and add it to the row
        Dim cell As New Cell(doc)
        cell.CellFormat.Shading.BackgroundPatternColor = Color.LightBlue
        cell.CellFormat.Width = 80

        ' Add a paragraph to the cell as well as a new run with some text.
        cell.AppendChild(New Paragraph(doc))
        cell.FirstParagraph.AppendChild(New Run(doc, "Row 1, Cell 1 Text"))

        ' Add the cell to the row.
        row.AppendChild(cell)

        ' We would then repeat the process for the other cells and rows in the table.
        ' We can also speed things up by cloning existing cells and rows.
        row.AppendChild(cell.Clone(False))
        row.LastCell.AppendChild(New Paragraph(doc))
        row.LastCell.FirstParagraph.AppendChild(New Run(doc, "Row 1, Cell 2 Text"))
        dataDir = dataDir & Convert.ToString("Table.InsertTableUsingNodes_out.doc")
        ' Save the document to disk.
        doc.Save(dataDir)
        ' ExEnd:InsertTableDirectly
        Console.WriteLine(Convert.ToString(vbLf & "Table using notes inserted successfully." & vbLf & "File saved at ") & dataDir)
    End Sub

End Class
