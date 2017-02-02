package gameServer

import "github.com/gorilla/websocket"
import "gameServer/models"
import "fmt"
import "log"

type Client struct{
    Connection *websocket.Conn
    Id int
}

func NewClient(conn *websocket.Conn) (client *Client){
    client = new(Client)
    client.Connection = conn
    return client
}
func (client *Client) SendCommand(cmd *models.Command) {
    var write_err error = nil
    if write_err = client.Connection.WriteMessage(websocket.TextMessage, cmd.GetRawData())
    write_err != nil {
        fmt.Printf("Send error: ")
        log.Println(write_err)
        return
    }
    fmt.Println("Send command successful")
}
