Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core

	Public MustInherit Class DataBaseFunctionsBase

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
		''' Führt ein "optimize table" für die übergebene Tabelle aus.
		''' </summary>
		Public MustOverride Function OptimizeTable _
		(ByVal connectionString As String _
		, ByVal table As String) As String
#End Region

	End Class
End Namespace