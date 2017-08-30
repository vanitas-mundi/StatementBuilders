Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Enums
#End Region

Namespace Core

	Public Class TableInfo

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _database As String = ""
		Private _table As String = ""
		Private _tableName As String = ""
		Private _alias As String = ""
		Private _joinType As JoinTypes = JoinTypes.None
		Private ReadOnly _constraints As New List(Of Constraint)
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property Database() As String
			Get
				Return _database
			End Get
			Set(ByVal value As String)
				_database = value
			End Set
		End Property

		Public Property Table() As String
			Get
				Return _table
			End Get
			Set(ByVal value As String)
				_table = value
			End Set
		End Property

		Public Property TableName() As String
			Get
				Return _tableName
			End Get
			Set(ByVal value As String)
				_tableName = value
			End Set
		End Property

		Public Property [Alias]() As String
			Get
				Return _alias
			End Get
			Set(ByVal value As String)
				_alias = value
			End Set
		End Property

		Public Property JoinType() As JoinTypes
			Get
				Return _joinType
			End Get
			Set(ByVal value As JoinTypes)
				_joinType = value
			End Set
		End Property

		Public ReadOnly Property Constraints() As List(Of Constraint)
			Get
				Return _constraints
			End Get
		End Property
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
#End Region

	End Class
End Namespace