Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports Devart.Data.MySql
Imports SSP.Data.StatementBuildersBase.Core
#End Region

Namespace Core

	Public Class DeleteBuilderMySql

		Inherits DeleteBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New("#")
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> �ffentliche Methoden der Klasse "
		'''<summary>F�hrt das L�sch-Statement unter Verwendung der Default-Connection aus.</summary>
		Public Overrides Function ExecuteNonQuery() As Int64
			Return ExecuteNonQuery(DbResultMySql.DefaultConnectionString)
		End Function

		'''<summary>F�hrt das L�sch-Statement aus.</summary>
		Public Overrides Function ExecuteNonQuery(ByVal connectionString As String) As Int64

			If Parameters.Count = 0 Then
				Return DbResultMySql.Instance.ExecuteNonQuery(connectionString, ToString)
			Else
				Return DbResultMySql.Instance.ExecuteNonQuery(connectionString, ToString _
				, Parameters.GetIDbParameters(Of MySqlParameter))
			End If
		End Function
#End Region

	End Class

End Namespace