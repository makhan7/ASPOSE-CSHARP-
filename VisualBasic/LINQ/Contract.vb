﻿
Imports System.Collections.Generic
Imports System.Text

Namespace LINQ
    ' ExStart:Contract
    Public Class Contract
        Public Property Manager() As Manager
            Get
                Return m_Manager
            End Get
            Set(value As Manager)
                m_Manager = value
            End Set
        End Property
        Private m_Manager As Manager
        Public Property Client() As Client
            Get
                Return m_Client
            End Get
            Set(value As Client)
                m_Client = value
            End Set
        End Property
        Private m_Client As Client
        Public Property Price() As Single
            Get
                Return m_Price
            End Get
            Set(value As Single)
                m_Price = value
            End Set
        End Property
        Private m_Price As Single
        Public Property [Date]() As DateTime
            Get
                Return m_Date
            End Get
            Set(value As DateTime)
                m_Date = value
            End Set
        End Property
        Private m_Date As DateTime
    End Class
    ' ExEnd:Contract
End Namespace

