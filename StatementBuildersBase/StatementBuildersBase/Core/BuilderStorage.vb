Option Explicit On
Option Strict On
Option Infer On

#Region " --------------->> Imports/ usings "
Imports SSP.Data.StatementBuildersBase.Core.Interfaces
#End Region

Namespace Core

	Public Class BuilderStorage

		Implements IEnumerable(Of IBuilderBase)

#Region " --------------->> Enumerationen der Klasse "
#End Region

#Region " --------------->> Eigenschaften der Klasse "
		Private ReadOnly _builders As New Dictionary(Of String, IBuilderBase)
#End Region

#Region " --------------->> Konstruktor und Destruktor der Klasse "
#End Region

#Region " --------------->> Zugriffsmethoden der Klasse "
		Public ReadOnly Property Item _
		 (ByVal name As String) As IBuilderBase
			Get
				Return _builders.Item(name)
			End Get
		End Property

		Public ReadOnly Property Item _
		 (ByVal index As Int32) As IBuilderBase
			Get
				Return _builders.Values(index)
			End Get
		End Property

		Public ReadOnly Property Builder _
		 (ByVal name As String) As BuilderBase
			Get
				Return DirectCast(_builders.Item(name), BuilderBase)
			End Get
		End Property

		Public ReadOnly Property Builder _
		 (ByVal index As Int32) As BuilderBase
			Get
				Return DirectCast(_builders.Values(index), BuilderBase)
			End Get
		End Property

		Public ReadOnly Property Count() As Int32
			Get
				Return _builders.Count
			End Get
		End Property

		Public ReadOnly Property BuilderNames() As String()
			Get
				Return _builders.Keys.ToArray
			End Get
		End Property

		Public ReadOnly Property SelectBuilders() _
		 As SelectBuilderBase()
			Get
				Dim ret = From item In _builders.Values
									Where TypeOf item Is SelectBuilderBase
									Select DirectCast(item, SelectBuilderBase)
				Return ret.ToArray
			End Get
		End Property

		Public ReadOnly Property DeleteBuilders() _
		 As DeleteBuilderBase()
			Get
				Dim ret = From item In _builders.Values
									Where TypeOf item Is DeleteBuilderBase
									Select DirectCast(item, DeleteBuilderBase)
				Return ret.ToArray
			End Get
		End Property

		Public ReadOnly Property InsertBuilders() _
		 As InsertBuilderBase()
			Get
				Dim ret = From item In _builders.Values
									Where TypeOf item Is InsertBuilderBase
									Select DirectCast(item, InsertBuilderBase)
				Return ret.ToArray
			End Get
		End Property

		Public ReadOnly Property UpdateBuilders() _
		 As UpdateBuilderBase()
			Get
				Dim ret = From item In _builders.Values
									Where TypeOf item Is UpdateBuilderBase
									Select DirectCast(item, UpdateBuilderBase)
				Return ret.ToArray
			End Get
		End Property
#End Region

#Region " --------------->> Ereignismethoden Methoden der Klasse "
#End Region

#Region " --------------->> Private Methoden der Klasse "
		Private Function GetEnumerator1() _
		 As System.Collections.IEnumerator _
		 Implements System.Collections.IEnumerable.GetEnumerator

			Return _builders.Values.GetEnumerator
		End Function
#End Region

#Region " --------------->> Öffentliche Methoden der Klasse "
		Public Sub Add(ByVal builder As IBuilderBase)
			_builders.Add(DirectCast(builder, BuilderBase).Name, builder)
		End Sub

		Public Sub Remove(ByVal builder As BuilderBase)
			Remove(builder.Name)
		End Sub

		Public Sub Remove(ByVal name As String)
			_builders.Remove(name)
		End Sub

		Public Sub RemoveAt(ByVal index As Int32)
			Remove(_builders.Keys(index))
		End Sub

		Public Function GetEnumerator() _
		 As IEnumerator(Of IBuilderBase) _
		 Implements IEnumerable(Of IBuilderBase).GetEnumerator

			Return _builders.Values.GetEnumerator
		End Function
#End Region

	End Class
End Namespace