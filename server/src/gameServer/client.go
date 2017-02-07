package gameServer

import "github.com/gorilla/websocket"
import "gameServer/models"
import "fmt"
import "log"

// interfaces in go
// https://go-book.appspot.com/interfaces.html
type Client interface{
    SendCommand(cmd *models.Command)
}
type BaseClient struct{
    Connection *websocket.Conn
    Id int
}

func NewClient(conn *websocket.Conn) (client BaseClient){
    client.Connection = conn
    return client
}

func (client BaseClient) SendCommand(cmd models.Command) {
    var write_err error = nil
    if write_err = client.Connection.WriteMessage(websocket.TextMessage, cmd.GetRawData())
    write_err != nil {
        fmt.Printf("Send error: ")
        log.Println(write_err)
        return
    }
    fmt.Println("Send command successful")
}
