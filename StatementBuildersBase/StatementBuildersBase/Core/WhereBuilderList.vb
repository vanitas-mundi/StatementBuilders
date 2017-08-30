Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Base.ExtensionMethods
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public Class WhereBuilderList

		Inherits BuilderList

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New()
			MyBase.New("")
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Private Function GetLogicalOperatorString _
		(ByVal options As AddFieldAndValueOptions) As String

			Dim result = ""
			Select Case options.LogicalOperator
				Case LogicalOperators.None
					result = ""
				Case LogicalOperators.AndNot, LogicalOperators.OrNot, LogicalOperators.XorNot
					result = options.LogicalOperator.ToString.ToUpper.Replace("NOT", " NOT")
				Case Else
					result = options.LogicalOperator.ToString.ToUpper
			End Select
			Return If(options.Parenthesis = Parenthesis.LeftParenthesis, result & " (", result & " ")
		End Function

		Private Function GetCompareOperatorString _
		(ByVal options As AddFieldAndValueOptions) As String

			Select Case options.CompareOperator
				Case CompareOperators.Equal
					Return "= {0}"
				Case CompareOperators.[IsNull]
					Return "IS NULL"
				Case CompareOperators.IsNotNull
					Return "IS NOT NULL"
				Case CompareOperators.LesserThan
					Return "< {0}"
				Case CompareOperators.GreaterThan
					Return "> {0}"
				Case CompareOperators.LesserEqualThan
					Return "<= {0}"
				Case CompareOperators.GreaterEqualThan
					Return ">= {0}"
				Case CompareOperators.Unequal
					Return "<> {0}"
				Case CompareOperators.[Like]
					Return "LIKE {0}"
				Case CompareOperators.[NotLike]
					Return "NOT LIKE {0}"
				Case CompareOperators.[In]
					Return "IN({0})"
				Case CompareOperators.[NotIn]
					Return "NOT IN({0})"
				Case Else
					Throw New Exception("Invalid compareOperator!")
			End Select
		End Function
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Werte values in das Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal options As AddFieldAndValueOptions, ByVal values As TValue())

			Dim valueList = New List(Of String)
			For Each value In values
				If options.QuotingRules Is Nothing Then
					valueList.Add(Convert.ToString(value))
				Else
					valueList.Add(options.QuotingRules.GetQuotedValue(value))
				End If

			Next value

			options.QuotingRules = Nothing
			AddFieldAndValue(options, valueList.EnumerableJoin)
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal options As AddFieldAndValueOptions, ByVal value As TValue)

			Dim v = If(options.QuotingRules Is Nothing, Convert.ToString(value) _
			, options.QuotingRules.GetQuotedValue(value))

			options.QuotingRules = Nothing
			AddFieldAndValue(options, v)
		End Sub


		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''</summary>
		Public Sub AddFieldAndValue _
		(ByVal options As AddFieldAndValueOptions, ByVal value As String)

			Select Case options.CompareOperator
				Case CompareOperators.IsNull, CompareOperators.IsNotNull
					value = ""
				Case Else
					If options.QuotingRules IsNot Nothing Then
						value = options.QuotingRules.GetQuotedValue(value)
					End If
			End Select

			Dim logicalOperatorString = GetLogicalOperatorString(options)
			Dim operatorAndValue = String.Format(GetCompareOperatorString(options), value)
			Dim temp = String.Format("{0}({1} {2})" _
			, logicalOperatorString, options.ColumnName _
			, operatorAndValue).Trim.Replace(" )", ")")
			Me.Add(If(options.Parenthesis = Parenthesis.RightParenthesis, temp & ")", temp))
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Werte values in das Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''</summary>
		Public Sub AddFieldAndValue _
		(ByVal logicalOperator As LogicalOperators, ByVal columnName As String _
		, ByVal compareOperator As CompareOperators, ByVal values As String())

			Dim options = New AddFieldAndValueOptions With
			{.LogicalOperator = logicalOperator, .ColumnName = columnName _
			, .CompareOperator = compareOperator, .Parenthesis = Parenthesis.None _
			, .QuotingRules = Nothing}

			AddFieldAndValue(options, values.EnumerableJoin)
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Werte values in das Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal logicalOperator As LogicalOperators, ByVal columnName As String _
		, ByVal compareOperator As CompareOperators, ByVal values As TValue() _
		, ByVal quotingRules As IValueQuotingRules)

			Dim options = New AddFieldAndValueOptions With
			{.LogicalOperator = logicalOperator, .ColumnName = columnName _
			, .CompareOperator = compareOperator, .Parenthesis = Parenthesis.None _
			, .QuotingRules = quotingRules}

			AddFieldAndValue(options, values)
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Statement ein.
		'''Value muss bei der Übergabe datenbakkomform gequotet und maskiert sein.
		'''</summary>
		Public Sub AddFieldAndValue _
		(ByVal logicalOperator As LogicalOperators, ByVal columnName As String _
		, ByVal compareOperator As CompareOperators, ByVal value As String)

			Dim options = New AddFieldAndValueOptions With
			{.LogicalOperator = logicalOperator, .ColumnName = columnName _
			, .CompareOperator = compareOperator, .Parenthesis = Parenthesis.None _
			, .QuotingRules = Nothing}

			AddFieldAndValue(options, value)
		End Sub

		'''<summary>
		'''Fügt ein weiteres Feld columnName und dessen Wert value in das Statement ein.
		'''Value wird über das quotingRules-Objekt datenbakkomform gequotet und maskiert.
		'''</summary>
		Public Sub AddFieldAndValue(Of TValue As IConvertible) _
		(ByVal logicalOperator As LogicalOperators, ByVal columnName As String _
		, ByVal compareOperator As CompareOperators, ByVal value As TValue _
		, ByVal quotingRules As IValueQuotingRules)

			Dim options = New AddFieldAndValueOptions With
			{.LogicalOperator = logicalOperator, .ColumnName = columnName _
			, .CompareOperator = compareOperator, .Parenthesis = Parenthesis.None _
			, .QuotingRules = quotingRules}

			AddFieldAndValue(options, value)
		End Sub
#End Region

	End Class

End Namespace

