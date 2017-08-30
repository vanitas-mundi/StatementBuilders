Option Explicit On
Option Infer On
Option Strict On

#Region " --------------->> Imports/ usings "
#End Region

Namespace Core.Enums

	Public Enum FieldParserTypes
		FieldOnly = 0
		FieldAndAlias = 1
		FieldAndTable = 2
		FieldTableAndDatabase = 3
	End Enum

End Namespace