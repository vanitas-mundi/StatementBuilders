Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public Class BuilderDictionary

		Inherits Dictionary(Of String, String)

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		''' <summary>
		''' 
		''' </summary>
		''' <param name="columnName">Der Feldname in der Datentabelle.</param>
		''' <param name="value">
		''' 	Der Wert, welcher gesetzt werden soll 
		''' 	(inklusive Maskierung z.B. 'Wert' anstatt Wert).
		''' </param>
		Public Overloads Sub Add(ByVal columnName As String, ByVal value As String)
			MyBase.Add(columnName, value)
		End Sub
#End Region

	End Class

End Namespace

