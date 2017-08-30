Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public Class AddFieldAndValueOptions

#Region " --------------->> Enumerationen der Klasse "
#End Region '{Enumerationen der Klasse}

#Region " --------------->> Eigenschaften der Klasse "
		Private _logicalOperator As LogicalOperators = LogicalOperators.None
		Private _columnName As String = ""
		Private _compareOperator As CompareOperators = CompareOperators.Equal
		Private _quotingRules As IValueQuotingRules = Nothing
		Private _parenthesis As Parenthesis = Parenthesis.None
#End Region '{Eigenschaften der Klasse}

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region '{Konstruktor und Destruktor der Klasse}

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public Property LogicalOperator As LogicalOperators
			Get
				Return _logicalOperator
			End Get
			Set(value As LogicalOperators)
				_logicalOperator = value
			End Set
		End Property

		Public Property ColumnName As String
			Get
				Return _columnName
			End Get
			Set(value As String)
				_columnName = value
			End Set
		End Property

		Public Property CompareOperator As CompareOperators
			Get
				Return _compareOperator
			End Get
			Set(value As CompareOperators)
				_compareOperator = value
			End Set
		End Property

		Public Property QuotingRules As IValueQuotingRules
			Get
				Return _quotingRules
			End Get
			Set(value As IValueQuotingRules)
				_quotingRules = value
			End Set
		End Property

		Public Property Parenthesis As Parenthesis
			Get
				Return _parenthesis
			End Get
			Set(value As Parenthesis)
				_parenthesis = value
			End Set
		End Property
#End Region '{Zugriffsmethoden der Klasse}

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region '{Ereignismethoden der Klasse}

#Region " --------------->> Private Methoden der Klasse "
#End Region '{Private Methoden der Klasse}

#Region " --------------->> Öffentliche Methoden der Klasse "
#End Region '{Öffentliche Methoden der Klasse}

	End Class

End Namespace
