Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
Imports System.Text.RegularExpressions
Imports System.Text
Imports SSP.Data.StatementBuildersBase.Core.Enums
#End Region

Namespace Core

	Public Class StatementBuilderBase

		Inherits BuilderBase
		Implements IBuilderBase

#Region " --------------->> Enumerationen der Klasse "
		Public Enum StatementTypes
			[Select] = 0
			Insert = 1
			Update = 2
			Delete = 3
			Others = 4
		End Enum
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private _containsFrom As Boolean
		Private _containsWhere As Boolean
		Private _containsGroup As Boolean
		Private _containsHaving As Boolean
		Private _containsOrder As Boolean

		Private ReadOnly _select As New BuilderList(",")
		Private ReadOnly _from As New BuilderList("")
		Private ReadOnly _tables As New List(Of TableInfo)
		Private ReadOnly _where As New BuilderList("")
		Private ReadOnly _group As New BuilderList(",")
		Private ReadOnly _having As New BuilderList("")
		Private ReadOnly _order As New BuilderList(",")
		Private _statementType As StatementTypes
		Private _statement As String
		Private ReadOnly _masks As New Dictionary(Of String, String)

#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
		Public Sub New(ByVal commentChar As String)
			MyBase.New(commentChar)
		End Sub

		Public Sub New(ByVal commentChar As String, ByVal statement As String)
			MyBase.New(commentChar)
			DivideStatement(EraseComments(statement))
		End Sub
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public ReadOnly Property StatementType() As StatementTypes
			Get
				Return _statementType
			End Get
		End Property

		Public ReadOnly Property Tables() As List(Of TableInfo)
			Get
				Return _tables
			End Get
		End Property

		Public ReadOnly Property [Select] As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _select
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property

		Public ReadOnly Property From As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _from
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property

		Public ReadOnly Property Where As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _where
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property

		Public ReadOnly Property Group As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _group
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property

		Public ReadOnly Property Having As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _having
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property

		Public ReadOnly Property Order As BuilderList
			Get
				If _statementType = StatementTypes.Select Then
					Return _order
				Else
					Throw New Exception("Auflistung nur bei Auswahlabfragen zugänglich.")
				End If
			End Get
		End Property
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Private Function GetStatementOnly() As String

			If Not _statementType = StatementTypes.Select Then Return _statement

			Dim sb = New StringBuilder

			sb.AppendLine("SELECT")
			For Each s As String In _select
				sb.AppendLine(vbTab & s & ",")
			Next s
			If Not _select.Count = 0 Then sb.Remove(sb.Length - 3, 1)

			sb.AppendLine("FROM")
			For Each s As String In _from
				sb.AppendLine(vbTab & s)
			Next s

			If Not _where.Count = 0 Then
				sb.AppendLine("WHERE")
				For Each s As String In _where
					sb.AppendLine(vbTab & s)
				Next s
			End If

			If Not _group.Count = 0 Then
				sb.AppendLine("GROUP BY")
				For Each s As String In _group
					sb.AppendLine(vbTab & s & ",")
				Next s
				If Not _group.Count = 0 Then sb.Remove(sb.Length - 3, 1)
			End If

			If Not _having.Count = 0 Then
				sb.AppendLine("HAVING")
				For Each s As String In _having
					sb.AppendLine(vbTab & s)
				Next s
			End If

			If Not _order.Count = 0 Then
				sb.AppendLine("ORDER BY")
				For Each s As String In _order
					sb.AppendLine(vbTab & s & ",")
				Next s
				If Not _order.Count = 0 Then sb.Remove(sb.Length - 3, 1)
			End If
			Return sb.ToString
		End Function

		Private Sub DivideStatement(ByVal statement As String)

			Dim statementWithoutWhitespace = Regex.Replace _
					(Regex.Replace(statement, "\s", " "), "[ ]+", " ")

			Dim temp = New StringBuilder(" " & statementWithoutWhitespace)

			Dim divideString = temp.ToString
			Dim s = divideString.ToLower

			_containsFrom = s.Contains(" from ")
			_containsWhere = s.Contains(" where ")
			_containsGroup = s.Contains(" group by ")
			_containsHaving = s.Contains(" having ")
			_containsOrder = s.Contains(" order by ")

			Select Case True
				Case s.Contains(" select ")
					_statementType = StatementTypes.Select
					DivideSelect(divideString)
					DivideFrom(divideString)
					DivideWhere(divideString)
					DivideGroup(divideString)
					DivideHaving(divideString)
					DivideOrder(divideString)

				Case s.Contains(" insert ")
					_statementType = StatementTypes.Insert
					_statement = statement

				Case s.Contains(" update ")
					_statementType = StatementTypes.Update
					_statement = statement

				Case s.Contains(" delete ")
					_statement = statement
					_statementType = StatementTypes.Delete

				Case Else
					_statementType = StatementTypes.Others
					_statement = statement
			End Select
		End Sub

		Private Sub DivideSelect(ByVal divideString As String)

			Dim ok = (_containsFrom) _
					 OrElse (Not _containsFrom _
							 AndAlso Not _containsWhere _
							 AndAlso Not _containsGroup _
							 AndAlso Not _containsHaving _
							 AndAlso Not _containsOrder) _
					 OrElse (Not _containsFrom _
							 AndAlso Not _containsWhere _
							 AndAlso Not _containsGroup _
							 AndAlso Not _containsHaving _
							 AndAlso _containsOrder)

			If Not ok Then Throw New Exception("FROM-Zweig erwartet oder Fehler im FROM-Zweig.")

			Dim startPos = divideString.ToLower.IndexOf(" select ") + 8
			Dim selectString = divideString.Substring(startPos)

			Select Case True
				Case _containsFrom
					Dim endPos = selectString.ToLower.IndexOf(" from ")
					selectString = selectString.Substring(0, endPos)
				Case _containsOrder
					Dim endPos = selectString.ToLower.IndexOf(" order by ")
					selectString = selectString.Substring(0, endPos)
			End Select

			CheckBrackets(selectString, "SELECT")
			selectString = MaskString(selectString)

			Dim selectStringCollection() = selectString.Split(","c)

			For Each s As String In selectStringCollection
				_select.Add(DeMaskString(s).Trim)
			Next s
		End Sub

		'Maskiert Funktionen wie Concat
		Private Function MaskString(ByRef s As String) As String

			_masks.Clear()
			Return Regex.Replace(s, "\(.*?\)", AddressOf MaskMatch)
		End Function

		Private Function MaskMatch(ByVal m As Match) As String

			_masks.Add("<match" & m.Index & ">", m.Value)
			Return "<match" & m.Index & ">"
		End Function

		Private Function DeMaskString(ByVal s As String) As String

			Return Regex.Replace(s, "<match.*?>", AddressOf UnMaskMatch)
		End Function

		Private Function UnMaskMatch(ByVal m As Match) As String
			Return _masks.Item(m.Value)
		End Function

		Private Sub ChopStatement(ByVal sb As StringBuilder)

			Dim s = sb.ToString.ToLower
			Dim pos As Int32

			Select Case True
				Case s.Contains(" where ")
					pos = sb.ToString.ToLower.IndexOf(" where ")
				Case s.Contains(" group by ")
					pos = sb.ToString.ToLower.IndexOf(" group by ")
				Case s.Contains(" having ")
					pos = sb.ToString.ToLower.IndexOf(" having ")
				Case s.Contains(" order by ")
					pos = sb.ToString.ToLower.IndexOf(" order by ")
				Case Else
					Return
			End Select

			sb.Remove(pos, sb.Length - pos)
		End Sub

		Private Sub DivideFromWithOutJoins(ByVal sb As StringBuilder)

			Dim tables As String() = sb.ToString.Split(","c)

			For i = 0 To tables.Count - 1
				Dim item = tables(i).Trim
				Dim ti As New TableInfo

				'temp(0) enthält Datenbank und Tabelle temp(1) Alias
				Dim temp = item.Split(" "c)
				ti.JoinType = JoinTypes.None

				If temp.Count > 1 Then ti.Alias = temp(1)

				'Ist count 1 dann temp(0) = Tabelle bei 2 temp(0) = Datenbank; temp(1) = Tabelle
				temp = temp(0).Split("."c)

				Select Case temp.Count
					Case 1
						ti.Database = ""
						ti.Table = temp(0)
					Case 2
						ti.Database = temp(0)
						ti.Table = temp(1)
				End Select

				_tables.Add(ti)
				_from.Add(item)
			Next i
		End Sub

		''' <summary>
		''' Ermittelt den JoinType eines Joins (INNER, LEFT, RIGHT, FULL)
		''' </summary>
		Private Function GetTableInfoJoinType(ByVal fromItem As String) As JoinTypes

			Select Case True
				Case Regex.IsMatch(fromItem, " inner ", RegexOptions.IgnoreCase)
					Return JoinTypes.Inner
				Case Regex.IsMatch(fromItem, " left ", RegexOptions.IgnoreCase)
					Return JoinTypes.Left
				Case Regex.IsMatch(fromItem, " right ", RegexOptions.IgnoreCase)
					Return JoinTypes.Right
				Case Regex.IsMatch(fromItem, " full ", RegexOptions.IgnoreCase)
					Return JoinTypes.Full
				Case Else
					Return JoinTypes.None
			End Select
		End Function

		''' <summary>
		''' Ermittelt weitere Einschränkungen des Joins
		''' </summary>
		Private Sub GetTableInfoConstraints(ByVal fromItem As String, ByVal ti As TableInfo)

			Dim constraintsArray = Regex.Split(fromItem, " on ", RegexOptions.IgnoreCase)

			If constraintsArray.Count <= 1 Then Return
			'Keine weiteren Einschränkungen vorhanden

			Const pattern As String = "\band\b|\bor\b"
			Dim logicalOperators = New List(Of String)
			Dim temp = constraintsArray(1)

			logicalOperators.Add("")

			logicalOperators.AddRange(Regex.Matches(temp, pattern, RegexOptions.IgnoreCase).Cast _
			(Of Match).Select(Function(m) m.ToString.Trim.ToUpper).ToList)

			constraintsArray = Regex.Split(temp, pattern, RegexOptions.IgnoreCase)

			For i = 0 To constraintsArray.Count - 1
				Dim item = constraintsArray(i).Trim

				'Bsp Item: p.personenid = datapool.t_anschriften.personenfid inner
				Dim tempArray = item.Split("="c)
				'Ergebnis:
				'p.personenid
				'datapool.t_anschriften.personenfid inner

				'Klammern entfernen.
				Dim leftFullField = Regex.Replace(tempArray(0).Trim, "\(|\)", "")
				'Klammern und abschließendes INNER entfernen.
				Dim rightFullField = Regex.Replace(tempArray(1) _
													 , "\(|\)|inner", "", RegexOptions.IgnoreCase).Trim

				Dim constraint = New Constraint _
						(leftFullField, rightFullField, logicalOperators.Item(i))
				ti.Constraints.Add(constraint)
			Next i
		End Sub

		''' <summary>
		''' Ermittelt den Alias der verknüpften Tabelle.
		''' </summary>
		Private Function GetTableInfoAlias _
			(ByVal fromItem As String) As String

			Dim [alias] = ""
			If fromItem.Contains(" ") Then
				[alias] = fromItem.Trim.Substring(fromItem.Trim.IndexOf(" "c)).Trim
			End If

			If [alias].Contains(" ") Then
				[alias] = [alias].Substring(0, [alias].IndexOf(" "))
			Else
				Return ""
			End If

			If Regex.IsMatch([alias], "\bon\b", RegexOptions.IgnoreCase) Then
				Return ""
			Else
				Return [alias]
			End If
		End Function

		''' <summary>
		''' Liefert "", "INNER JOIN", "LEFT JOIN", "RIGHT JOIN" oder "FULL JOIN"
		''' </summary>
		Private Function GetJoinKey(ByVal fromItem As String) As String
			If Not fromItem.Contains(" ") Then
				Return ""
			Else
				Return fromItem.Substring(fromItem.LastIndexOf(" ")).ToUpper & " JOIN "
			End If
		End Function

    ''' <summary>
    ''' Formatiert den Eintrag zur Aufnahme in die FROM-List
    ''' </summary>
    Private Function GetJoinEntry(ByVal fromItem As String, ByVal lastJoinKey As String) As String

      'inner, left, right und full entfernen und JoinKey an den Anfang setzen
      Dim fromEntry = lastJoinKey & Regex.Replace(fromItem _
                            , "(\binner\b)|(\bleft\b)|(\bright\b)|(\bfull\b)" _
                            , "", RegexOptions.IgnoreCase)

      If Regex.IsMatch(fromEntry, "\bon\b") Then
        Return fromEntry.Substring(0, fromEntry.IndexOf(" on ")).Trim & vbCrLf _
             & "ON " & fromEntry.Substring(fromEntry.IndexOf(" on ") + 4).Trim
      Else
        Return fromEntry
      End If
    End Function

    Private Sub DivideFromWithJoins(ByVal sb As StringBuilder)

      Dim fromStringCollection = Regex.Split(sb.ToString, "\bjoin\b", RegexOptions.IgnoreCase)

      Dim lastJoinKey = String.Empty

      For Each fromItem In fromStringCollection
        fromItem = fromItem.Trim
        Dim ti = New TableInfo

        GetTableInfoConstraints(fromItem, ti)
        ti.TableName = fromItem.Substring(0, fromItem.IndexOf(" ")).Trim

        ti.Database = ti.TableName.Split("."c).First
        ti.Table = ti.TableName.Split("."c)(1)
        ti.Alias = GetTableInfoAlias(fromItem)
        ti.JoinType = GetTableInfoJoinType(fromItem)
        _tables.Add(ti)

        _from.Add(GetJoinEntry(fromItem, lastJoinKey))

        lastJoinKey = GetJoinKey(fromItem)
      Next fromItem
    End Sub

    Private Sub DivideFrom(ByVal divideString As String)

			If Not _containsFrom Then Return

      Dim temp = New StringBuilder(divideString.Substring(divideString.ToLower.IndexOf(" from ") + 6))

      ChopStatement(temp)
			CheckBrackets(temp.ToString, "FROM")

			Dim s = temp.ToString.ToLower

      If (Not s.Contains(" inner ")) _
      AndAlso (Not s.Contains(" left ")) _
      AndAlso (Not s.Contains(" right ")) _
      AndAlso (Not s.Contains(" full ")) Then
        DivideFromWithOutJoins(temp)
      Else
        DivideFromWithJoins(temp)
      End If
    End Sub

		Private Sub DivideWhere(ByVal divideString As String)

			If Not _containsWhere Then Return

      Dim temp = FormatAndOr(New StringBuilder(divideString.Substring(divideString.ToLower.IndexOf(" where ") + 7)))

      ChopStatement(temp)
			CheckBrackets(temp.ToString, "WHERE")

      Dim andCollection = Regex.Split(temp.ToString, "\band\b", RegexOptions.IgnoreCase)
      Dim [operator] = String.Empty

      For Each andItem In andCollection
        Dim orCollection = Regex.Split(andItem, "\bor\b", RegexOptions.IgnoreCase)

        For Each orItem In orCollection
          _where.Add($"{[operator]}{orItem.Trim}")
          [operator] = "OR "
        Next orItem

        [operator] = "AND "
			Next andItem
		End Sub

		Private Sub DivideGroup(ByVal divideString As String)

			If Not _containsGroup Then Return

      Dim temp = New StringBuilder(divideString.Substring(divideString.ToLower.IndexOf(" group by ") + 9))

      ChopStatement(temp)

			Dim groupString = temp.ToString
			CheckBrackets(groupString, "GROUP")

			groupString = MaskString(groupString)

			Dim groupCollection() = groupString.Split(","c)

			For Each groupItem As String In groupCollection
				_group.Add(DeMaskString(groupItem).Trim)
			Next groupItem
		End Sub

		Private Sub DivideHaving(ByVal divideString As String)

			If Not _containsHaving Then Return

			Dim temp = FormatAndOr(New StringBuilder(divideString.Substring(divideString.ToLower.IndexOf(" having ") + 8)))

			ChopStatement(temp)
			CheckBrackets(temp.ToString, "HAVING")

			Dim andCollection = Regex.Split(temp.ToString, "\band\b", RegexOptions.IgnoreCase)
			Dim [operator] = ""

			For Each andItem In andCollection
				Dim orCollection = Regex.Split(andItem, "\bor\b", RegexOptions.IgnoreCase)

				For Each orItem In orCollection
					_having.Add([operator] & orItem.Trim)
					[operator] = "OR "
				Next orItem

				[operator] = "AND "
			Next andItem
		End Sub

		Private Sub DivideOrder(ByVal divideString As String)

			If Not _containsOrder Then Return

      Dim orderString = divideString.Substring(divideString.ToLower.IndexOf(" order by ") + 9)

      CheckBrackets(orderString, "ORDER")

			orderString = MaskString(orderString)

			Dim orderStringCollection() = orderString.Split(","c)

			For Each orderItem In orderStringCollection
				_order.Add(DeMaskString(orderItem).Trim)
			Next orderItem
		End Sub

		Function CapAndOr(ByVal m As Match) As String

			Return m.ToString.ToUpper
		End Function


    '''<summary>Formatiert Schreibweise von AND und OR einheitlich.</summary>
    Private Function FormatAndOr(ByVal sb As StringBuilder) As StringBuilder

      Return New StringBuilder(Regex.Replace(sb.ToString, "\band\b|\bor\b", AddressOf CapAndOr, RegexOptions.IgnoreCase))
    End Function

    '''<summary>Prüft auf gleiche Anzahl von "(" und ")".</summary>
    Private Sub CheckBrackets(ByVal s As String, ByVal statementPart As String)

			Select Case Regex.Matches(s, "\(").Count - Regex.Matches(s, "\)").Count
				Case Is < 0
					Throw New Exception("'(' erwartet im " & statementPart & "-Zweig.")
				Case Is > 0
					Throw New Exception("')' erwartet " & statementPart & "-Zweig.")
			End Select
		End Sub

#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
    Public Overrides Function ToString() As String Implements IBuilderBase.ToString

      Return GetStatement(StatementFormats.StatementAndMetaData)
    End Function

    Public Overloads Function ToString(ByVal statementFormat As StatementFormats) As String _
    Implements IBuilderBase.ToString

      Return GetStatement(statementFormat)
    End Function

    Public Function GetStatement() As String Implements IBuilderBase.GetStatement

      Return GetStatement(StatementFormats.StatementAndMetaData)
    End Function

    Public Function GetStatement(ByVal statementFormat As StatementFormats) As String Implements IBuilderBase.GetStatement

      Dim sb As StringBuilder
      Select Case statementFormat
        Case StatementFormats.StatementAndMetaData
          sb = New StringBuilder(MyBase.ToString)
          sb.Append(GetStatementOnly)
        Case StatementFormats.MetaDataOnly
          sb = New StringBuilder(MyBase.ToString)
        Case StatementFormats.StatementOnly
          sb = New StringBuilder()
          sb.Append(GetStatementOnly)
        Case Else
          sb = New StringBuilder()
      End Select

      Return sb.ToString
    End Function

    Public Shared Function EraseComments(ByVal s As String) As String

      If Not Regex.Matches(s, "/\*").Count = Regex.Matches(s, "\*/").Count Then
        Throw New Exception("Fehler bei mehrzeiligem Kommentar!")
      End If

      Const pattern As String = "(/\*.*?\*/)|(#.*?\r\n)|(--.*?\r\n)"
			Return Regex.Replace(s, pattern, "")
		End Function

#End Region

	End Class

End Namespace