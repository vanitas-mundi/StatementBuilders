Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core
Imports System.Text
Imports System.Data.SqlClient
#End Region

Namespace Core

	Public Class SelectBuilderMsSql

		Inherits SelectBuilderBase

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _limit As Int32 = 0
		Private _limitPosition As Int32 = 0
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
		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings.</summary>
		Public Overrides Function ExecuteReader() As IDataReader
			Return ExecuteReader(DbResultMsSql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteReader(ByVal connectionString As String) As IDataReader

			If Me.Parameters.Count = 0 Then
				Return DbResultMsSql.Instance.ExecuteReader(connectionString, Me.ToString)
			Else
				Return DbResultMsSql.Instance.ExecuteReader _
				(connectionString, Me.ToString _
				, Me.Parameters.GetIDbParameters(Of SqlParameter))
			End If
		End Function

		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings.</summary>
		Public Overrides Function ExecuteScalar() As Object
			Return ExecuteScalar(DbResultMsSql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteScalar(ByVal connectionString As String) As Object

			Select Case Me.Parameters.Count
				Case 0
					Return DbResultMsSql.Instance.ExecuteScalar(connectionString, Me.ToString)
				Case Else
					Return DbResultMsSql.Instance.ExecuteScalar(connectionString, Me.ToString _
					, Me.Parameters.GetIDbParameters(Of SqlParameter))
			End Select
		End Function

		'''<summary>Führt das Select-Statement aus unter Verwendung des Default-ConnectionStrings aus.</summary>
		Public Overrides Function ExecuteStringScalar() As String
			Return ExecuteStringScalar(DbResultMsSql.DefaultConnectionString)
		End Function

		'''<summary>Führt das Select-Statement aus.</summary>
		Public Overrides Function ExecuteStringScalar(ByVal connectionString As String) As String

			Select Case Me.Parameters.Count
				Case 0
					Return DbResultMsSql.Instance.ExecuteStringScalar(connectionString, Me.ToString)
				Case Else
					Return DbResultMsSql.Instance.ExecuteScalar(connectionString, Me.ToString _
					, Me.Parameters.GetIDbParameters(Of SqlParameter)).ToString
			End Select
		End Function
#End Region

	End Class

End Namespace