Imports Microsoft.VisualBasic
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports Aspose.Words
Imports Aspose.Words.Rendering

Namespace DocumentExplorerExample
	''' <summary>
	''' Provides a utility method to print preview and print an Aspose.Words document.
	''' </summary>
	Friend Class Preview
		''' <summary>
		''' No ctor.
		''' </summary>
		Private Sub New()
		End Sub

		''' <summary>
		''' A utility method to print preview and print an Aspose.Words document.
		''' </summary>
		Friend Shared Sub Execute(ByVal document As Document)
			' This operation can take some time (for the first page) so we set the Cursor to WaitCursor.
			Dim cursor As Cursor = Cursor.Current
			Cursor.Current = Cursors.WaitCursor

			Dim previewDlg As New PrintPreviewDialog()

			' Initialize the Print Dialog with the number of pages in the document.
			Dim printDlg As New PrintDialog()
			printDlg.AllowSomePages = True
			printDlg.PrinterSettings = New PrinterSettings()
			printDlg.PrinterSettings.MinimumPage = 1
			printDlg.PrinterSettings.MaximumPage = document.PageCount
			printDlg.PrinterSettings.FromPage = 1
			printDlg.PrinterSettings.ToPage = document.PageCount

			' Restore cursor.
			Cursor.Current = cursor

			' Interesting, but PrintDialog will not show and will always return cancel
			' If you run this application in 64-bit mode.
			If (Not printDlg.ShowDialog().Equals(DialogResult.OK)) Then
				Return
			End If

			' Create the Aspose.Words' Implementation of the .NET print document 
			' And pass the printer settings from the dialog to the print document.
			Dim awPrintDoc As New AsposeWordsPrintDocument(document)
			awPrintDoc.PrinterSettings = printDlg.PrinterSettings

			' Pass the Aspose.Words' Print document to the .NET Print Preview dialog.
			previewDlg.Document = awPrintDoc

			previewDlg.ShowDialog()
		End Sub

	End Class
End Namespace