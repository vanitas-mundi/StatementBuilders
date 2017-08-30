Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports System.Data.SqlClient
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class DeleteBuilderMsSql

		Inherits DeleteBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New("/*", "*/")
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>Führt das Lösch-Statement unter Verwendung der Default-Connection aus.</summary>
		Public Overrides Function ExecuteNonQuery() As Int64
			Return ExecuteNonQuery(DbResultMsSql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Lösch-Statement aus.</summary>
		Public Overrides Function ExecuteNonQuery(ByVal connectionString As String) As Int64

			If Parameters.Count = 0 Then
				Return DbResultMsSql.Instance.ExecuteNonQuery(connectionString, ToString)
			Else
				Return DbResultMsSql.Instance.ExecuteNonQuery(connectionString, ToString _
				, Parameters.GetIDbParameters(Of SqlParameter))
			End If
		End Function
#End Region

	End Class

End Namespace