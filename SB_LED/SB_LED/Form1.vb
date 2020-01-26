Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form1
    Shared _continue As Boolean
    Shared _serialPort As SerialPort

    Dim comPORT As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        comPORT = ""
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next



    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If (ComboBox1.SelectedItem <> "") Then
            comPORT = ComboBox1.SelectedItem
        End If
    End Sub


    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        If (Button11.Text = "Connect") Then
            If (comPORT <> "") Then
                SerialPort1.Close()
                'SerialPort1.PortName = "com7" 'change com port to match your Arduino port
                SerialPort1.PortName = comPORT
                SerialPort1.BaudRate = 9600
                SerialPort1.DataBits = 8
                SerialPort1.Parity = Parity.None
                SerialPort1.StopBits = StopBits.One
                SerialPort1.Handshake = Handshake.None
                SerialPort1.Encoding = System.Text.Encoding.Default

                SerialPort1.Open()
                Button11.Text = "Dis-connect"

                '連接COM Port 先將單一關門功能鎖住
                Button4.Enabled = False
            End If

        Else
            SerialPort1.Close()
            Button11.Text = "Connect"
            RichTextBox1.Text = ""

        End If

    End Sub

    'Auto Open 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'SerialPort1.Open()
        SerialPort1.Write("0")
        'SerialPort1.Close()

        '治具打開無動作時可以做單一動作檢查控制
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button10.Enabled = True
        RichTextBox1.Text &= "Box Auto Open..." & vbCrLf

    End Sub

    'Auto Close 
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'SerialPort1.Open()
        SerialPort1.Write("1")
        'SerialPort1.Close()

        '治具關起來時關閉所有單一檢查控制功能
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False

        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
        RichTextBox1.Text &= "Box Auto Close..." & vbCrLf

    End Sub

    'Plate Insert
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'SerialPort1.Open()
        SerialPort1.Write("3")
        'SerialPort1.Close()

        '載板進入治具關閉開關門功能
        Button1.Enabled = False
        Button2.Enabled = False
        Button4.Enabled = False
        Button8.Enabled = False
        RichTextBox1.Text &= "Plate Insert..." & vbCrLf
    End Sub

    'Door Close
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'SerialPort1.Open()
        SerialPort1.Write("5")
        'SerialPort1.Close()

        'Door Close 關閉 DUT Dis-Link 避免誤觸動作並開啟 Door Open功能
        Button8.Enabled = True
        Button10.Enabled = False
        RichTextBox1.Text &= "Door Close..." & vbCrLf

    End Sub

    'Probe Contact
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'SerialPort1.Open()
        SerialPort1.Write("7")
        'SerialPort1.Close()

        'Probe Contact 時關閉所有Auto Close/Open動作並關閉Plate Back功能避免誤觸動作
        Button1.Enabled = False
        Button2.Enabled = False
        Button4.Enabled = False
        Button8.Enabled = False
        Button7.Enabled = False

        RichTextBox1.Text &= "Probe Contact..." & vbCrLf
    End Sub

    'Ping DUT
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'SerialPort1.Open()
        SerialPort1.Write("9")
        'SerialPort1.Close()

        'Ping DUT 開啟 Door Open動作並關閉Close/Open動作並關閉Probe Up功能避免誤觸動作
        Button1.Enabled = False
        Button2.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False

        Button4.Enabled = True

        RichTextBox1.Text &= "Ping DUT..." & vbCrLf
    End Sub

    'Plate Back
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'SerialPort1.Open()
        SerialPort1.Write("2")
        'SerialPort1.Close()

        'Plate Back 關閉 Door Close動作並開啟Close/Open功能
        Button1.Enabled = True
        Button2.Enabled = True
        Button8.Enabled = True

        Button4.Enabled = False
        RichTextBox1.Text &= "Plate Back..." & vbCrLf
    End Sub

    'Door Open
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'SerialPort1.Open()
        SerialPort1.Write("4")
        'SerialPort1.Close()

        'Door Open 開啟 DUT Dis-Link  
        Button10.Enabled = True
        RichTextBox1.Text &= "Door Open..." & vbCrLf
    End Sub

    'Probe Up
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'SerialPort1.Open()
        SerialPort1.Write("6")
        'SerialPort1.Close()

        'Probe Up 開啟 Plate Back  
        Button7.Enabled = True
        RichTextBox1.Text &= "Probe Up..." & vbCrLf

    End Sub

    'DUT Dis-Link
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'SerialPort1.Open()
        SerialPort1.Write("8")
        'SerialPort1.Close()

        ' DUT-Dis Link 開啟 Probe Up 
        Button9.Enabled = True
        RichTextBox1.Text &= "DUT Dis-Link..." & vbCrLf

    End Sub


End Class
