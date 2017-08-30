Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports "
Imports SSP.Data.StatementBuildersBase.Core
Imports System.Text
#End Region

Namespace Core

	Public Class UpdateBuilderAD

		Inherits UpdateBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
		Private _table As String
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New(Nothing)
		End Sub
#End Region  '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property Table As String
			Get
				Return _table
			End Get
			Set(value As String)
				_table = value
			End Set
		End Property
#End Region  '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region  '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
		Protected Overrides Function GetStatementOnly() As String
			Dim sb = New StringBuilder
			sb.AppendLine("UPDATE")
			sb.AppendLine(vbTab & Me.Table)

			sb.AppendLine(MyBase.GetStatementOnly)
			Return sb.ToString
		End Function
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Update-Statement unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteNonQuery() As Int64
			Return ExecuteNonQuery(DbResultAD.DefaultConnectionString)
		End Function

		'''<summary>Führt das Update-Statement aus.</summary>
		Public Overrides Function ExecuteNonQuery(connectionString As String) As Int64
			Return DbResultAD.Instance.ExecuteNonQuery(connectionString, Me.ToString)
		End Function

#End Region '{Öffentliche Methoden der Klasse}

	End Class

End Namespace






