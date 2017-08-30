
Imports SSP.Data.StatementBuildersBase.Core
Imports SSP.Data.StatementBuildersBase.Core.Enums
Imports SSP.Data.StatementBuildersMsSql.Core
Imports SSP.Data.StatementBuildersMySql.Core

Module Module1

    'Private _connectionString As String = "host=sql-unittest;uid=apps;pwd=bcw;unicode=true;"
    Private _connectionString As String = "Data Source=(localDB)\MSSQLLocalDB;Integrated Security= True;Initial Catalog=TestDB;"

    Private Sub ExecuteReaderTest()
        Dim sb = New SelectBuilderMySql
        sb.Select.Add("CONCAT(Nachname, ', ', Vorname) AS Fullanme, _rowid")
        sb.From.Add("datapool.t_personen")
        sb.Where.Add("Nachname = '{0}'", "glinka")


        Using dr = sb.ExecuteReader(_connectionString)
            While dr.Read
                Console.WriteLine(dr.Item(1))
            End While
        End Using

        Console.WriteLine(sb.ExecuteScalar(_connectionString))
    End Sub

    Private Sub SelectTest()
        Dim sb = New SelectBuilderMySql
        sb.Select.Add("CONCAT(Nachname, ', ', Vorname) AS Fullanme")
        sb.From.Add("datapool.t_personen")
        sb.Where.Add("_rowid = {0}", 27)

        sb.Comment = "Update Me"
        sb.Author = "Sascha Glinka"
        sb.Name = "Superupdate"
        sb.DateOfCreation = "2016-01-01"


        Dim options = New AddFieldAndValueOptions With
        {.LogicalOperator = LogicalOperators.AndNot, .ColumnName = "Nachname" _
        , .Parenthesis = Parenthesis.LeftParenthesis, .QuotingRules = DbResultMySql.QuotingRules}
        sb.Where.AddFieldAndValue(options, "Glinka")

        sb.Where.AddFieldAndValue(LogicalOperators.AndNot, "Nachname", CompareOperators.GreaterEqualThan, "Glinka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.OrNot, "Nachname", CompareOperators.GreaterThan, "Glinka", DbResultMySql.QuotingRules)

        options = New AddFieldAndValueOptions With
        {.LogicalOperator = LogicalOperators.Xor, .ColumnName = "Nachname" _
        , .CompareOperator = CompareOperators.In, .Parenthesis = Parenthesis.RightParenthesis _
        , .QuotingRules = DbResultMySql.QuotingRules}

        sb.Where.AddFieldAndValue(options, New Int32() {5, 9})


        sb.Where.AddFieldAndValue(LogicalOperators.XorNot, "Nachname", CompareOperators.IsNull, "Glinka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.IsNotNull, "Glinka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.Or, "Nachname", CompareOperators.LesserEqualThan, "Glinka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.LesserThan, "Glinka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Like, "Gl%ka%", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotIn, New String() {"Glinka", "Bert", "Gerd"}, DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotLike, "%ka", DbResultMySql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Unequal, "Glinka", DbResultMySql.QuotingRules)
        Console.WriteLine(sb.ToString)
        My.Computer.Clipboard.SetText(sb.ToString)
        Console.WriteLine(sb.ExecuteScalar(_connectionString))
    End Sub


    Private Sub SelectTest2()



        Dim sb = New SelectBuilderMsSql
        sb.Select.Add("name")
        sb.From.Add("TestTable")
        sb.Where.AddFieldAndValue(LogicalOperators.None, "id", CompareOperators.Equal, 1, DbResultMsSql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "name", CompareOperators.In, New String() {"glinka", "schmitz"}, DbResultMsSql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.Or, "validdate", CompareOperators.Equal, DateTime.Parse("14.04.1976"), DbResultMsSql.QuotingRules)
        sb.Where.AddFieldAndValue(LogicalOperators.And, "number", CompareOperators.Equal, Single.Parse("40,3"), DbResultMsSql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "decnumber", CompareOperators.Equal, "40,3", DbResultMsSql.QuotingRules)


        sb.Comment = "Selektiert Namen der Tabelle"
        sb.Author = "Sascha Glinka"
        sb.Name = "GetName"
        sb.DateOfCreation = "2016-01-01"


        'Dim options = New AddFieldAndValueOptions With
        '{.LogicalOperator = LogicalOperators.AndNot, .ColumnName = "Nachname" _
        ', .Parenthesis = Parenthesis.LeftParenthesis, .QuotingRules = DbResultMySql.QuotingRules}
        'sb.Where.AddFieldAndValue(options, "Glinka")

        'sb.Where.AddFieldAndValue(LogicalOperators.AndNot, "Nachname", CompareOperators.GreaterEqualThan, "Glinka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.OrNot, "Nachname", CompareOperators.GreaterThan, "Glinka", DbResultMySql.QuotingRules)

        'options = New AddFieldAndValueOptions With
        '{.LogicalOperator = LogicalOperators.Xor, .ColumnName = "Nachname" _
        ', .CompareOperator = CompareOperators.In, .Parenthesis = Parenthesis.RightParenthesis _
        ', .QuotingRules = DbResultMySql.QuotingRules}

        'sb.Where.AddFieldAndValue(options, New Int32() {5, 9})


        'sb.Where.AddFieldAndValue(LogicalOperators.XorNot, "Nachname", CompareOperators.IsNull, "Glinka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.IsNotNull, "Glinka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.Or, "Nachname", CompareOperators.LesserEqualThan, "Glinka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.LesserThan, "Glinka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Like, "Gl%ka%", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotIn, New String() {"Glinka", "Bert", "Gerd"}, DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotLike, "%ka", DbResultMySql.QuotingRules)
        'sb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Unequal, "Glinka", DbResultMySql.QuotingRules)
        Console.WriteLine(sb.ToString)
        Console.ReadKey(True)
        My.Computer.Clipboard.SetText(sb.ToString)
        Console.WriteLine(sb.ExecuteScalar(_connectionString))
    End Sub

    Private Sub UpdateTest()
        Dim usb = New UpdateBuilderMySql
        usb.UpdateTables.Add("datapool.t_personen")
        usb.AddFieldAndValue("_rowid", 3, DbResultMySql.QuotingRules)
        usb.AddFieldAndValue("Name", "Hans", DbResultMySql.QuotingRules)
        usb.AddFieldAndValue("Birth", DateTime.Parse("1.1.2000"), DbResultMySql.QuotingRules)
        usb.AddFieldAndValue(Of DBNull)("Vernichting", Convert.DBNull, DbResultMySql.QuotingRules)
    End Sub


    Private Sub UpdateTestMsSQL()



        '	UPDATE [dbo].[TestTable]
        '  SET [name] = <name, nchar(50),>
        '     ,[validdate] = <validdate, datetime,>
        '     ,[number] = <number, int,>
        '     ,[decnumber] = <decnumber, real,>
        'WHERE <Suchbedingungen,,>


        Dim usb = New UpdateBuilderMsSql
        usb.UpdateTables.Add("TestTable")
        usb.AddFieldAndValue("name", "Uwe", DbResultMsSql.QuotingRules)
        usb.AddFieldAndValue("validdate", DateTime.Now, DbResultMsSql.QuotingRules)
        usb.AddFieldAndValue("number", 75, DbResultMsSql.QuotingRules)
        usb.AddFieldAndValue("decnumber", Single.Parse("75,6"), DbResultMsSql.QuotingRules)
        usb.Where.AddFieldAndValue(LogicalOperators.None, "id", CompareOperators.Equal, 3, DbResultMsSql.QuotingRules)

        'usb.Where.AddFieldAndValue(LogicalOperators.Not, "Nachname", CompareOperators.Equal, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.AndNot, "Nachname", CompareOperators.GreaterEqualThan, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.OrNot, "Nachname", CompareOperators.GreaterThan, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.Xor, "Nachname", CompareOperators.In, New Int32() {5, 9}, DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.XorNot, "Nachname", CompareOperators.IsNull, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.IsNotNull, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.Or, "Nachname", CompareOperators.LesserEqualThan, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.LesserThan, "Glinka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Like, "Gl%ka%", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotIn, New String() {"Glinka", "Bert", "Gerd"}, DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotLike, "%ka", DbResultMySql.QuotingRules)
        'usb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Unequal, "Glinka", DbResultMySql.QuotingRules)

        Console.WriteLine(usb.ToString)
        Console.ReadKey()
        usb.ExecuteNonQuery()
        'usb.UpdateTables.Add("test.t_validation_tests")
        'usb.Set.Add("Name = '{0}'", "Rumpelpumpel")
        'usb.Where.Add("_rowid = {0}", 3)
        'Console.WriteLine(usb.ExecuteNonQuery(_connectionString))
    End Sub


    Private Sub InsertMsSQL()

        'INSERT INTO [dbo].[TestTable]
        '         ([name]
        '         ,[validdate]
        '         ,[number]
        '         ,[decnumber])
        '   VALUES
        '         (<name, nchar(50),>
        '         ,<validdate, datetime,>
        '         ,<number, int,>
        '         ,<decnumber, real,>)

        Dim isb = New InsertBuilderMsSql
        isb.Table = "TestTable"
        isb.AddFieldAndValue("name", "Hans", DbResultMsSql.QuotingRules)
        isb.AddFieldAndValue("validdate", DateTime.Parse("1976-04-14"), DbResultMsSql.QuotingRules)
        isb.AddFieldAndValue("number", 27, DbResultMsSql.QuotingRules)
        isb.AddFieldAndValue("decnumber", Single.Parse("14,8"), DbResultMsSql.QuotingRules)


        isb.ExecuteNonQuery()
        Console.WriteLine(isb.ToString)
        Console.ReadKey()
    End Sub

    Private Sub InsertTest()

        Dim isb = New InsertBuilderMySql
        isb.Table = "datapool.t_personen"
        isb.AddFieldAndValue("LastName", "Glinka", DbResultMySql.QuotingRules)
        isb.AddFieldAndValue("BirthDay", DateTime.Parse("14.04.76"), DbResultMySql.QuotingRules)
        isb.AddFieldAndValue("Age", 40, DbResultMySql.QuotingRules)
        isb.AddFieldAndValue(Of DBNull)("DeathDate", Convert.DBNull, DbResultMySql.QuotingRules)
        isb.AddFieldAndValue("IsMale", True, DbResultMySql.QuotingRules)
        isb.AddFieldAndValue("Size", Decimal.Parse("1,76"), DbResultMySql.QuotingRules)
        isb.Comment = "Das ist ein toller Insert!!!"
        Console.WriteLine(isb.ToString)
        Console.WriteLine()

        'isb.Table = "test.t_validation_tests"
        'isb.FieldsAndValues.Add("Name", "'Gerd'")
        'Console.WriteLine(isb.ExecutenonQuery(_connectionString))
    End Sub

    Private Sub DeleteTestMsSQL()

        'DELETE From [dbo].[TestTable]
        '    Where <Suchbedingungen,,>


        Dim dsb = New DeleteBuilderMsSql
        dsb.Table = "TestTable"
        dsb.Where.AddFieldAndValue(LogicalOperators.None, "name", CompareOperators.Equal, "uwe", DbResultMsSql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "id", CompareOperators.GreaterEqualThan, 3, DbResultMsSql.QuotingRules)
        'dsb.Author = "Riegelnig"
        'dsb.Comment = "Test"

        Console.WriteLine(dsb.ToString)
        Console.ReadKey()
        dsb.ExecuteNonQuery()
        'Console.WriteLine(dsb.ExecuteNonQuery(_connectionString))
    End Sub


    Private Sub DeleteTest()
        Dim dsb = New DeleteBuilderMySql
        dsb.Table = "test.t_validation_tests"
        dsb.Where.AddFieldAndValue(LogicalOperators.Not, "Nachname", CompareOperators.Equal, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.AndNot, "Nachname", CompareOperators.GreaterEqualThan, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.OrNot, "Nachname", CompareOperators.GreaterThan, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.Xor, "Nachname", CompareOperators.In, New Int32() {5, 9}, DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.XorNot, "Nachname", CompareOperators.IsNull, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.IsNotNull, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.Or, "Nachname", CompareOperators.LesserEqualThan, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.LesserThan, "Glinka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Like, "Gl%ka%", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotIn, New String() {"Glinka", "Bert", "Gerd"}, DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.NotLike, "%ka", DbResultMySql.QuotingRules)
        dsb.Where.AddFieldAndValue(LogicalOperators.And, "Nachname", CompareOperators.Unequal, "Glinka", DbResultMySql.QuotingRules)

        'LeftParenthesis
        'RightParenthesis
        Console.WriteLine(dsb.ToString)
        'Console.WriteLine(dsb.ExecuteNonQuery(_connectionString))
    End Sub

    Sub Main()

        'DbResultMySql.Initialize(_connectionString)
        DbResultMsSql.Initialize(_connectionString)

        DeleteTestMsSQL()
        'UpdateTestMsSQL()
        'InsertMsSQL()
        'Dim sb = New BCW.Foundation.Data.StatementBuilders.StatementBuildersBase.Core.SelectBuilderBase(Nothing)
        'SelectTest2()
        'UpdateTest()
        'InsertTest()
        'DeleteTest()
        'ExecuteReaderTest()
        Console.ReadKey()

    End Sub

End Module
