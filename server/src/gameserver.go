package main

import "net/http"
import "fmt"
import "github.com/gorilla/websocket"
import "encoding/json"
import "gameServer/models"

var upgrader = websocket.Upgrader{
    CheckOrigin: func(r *http.Request) bool {
        return true
    },
}

func main(){
    fmt.Printf("Server is running\n")
    http.HandleFunc("/", wsPage)
    http.ListenAndServe(":7777", nil)
}

func wsPage(response http.ResponseWriter, request *http.Request){
    conn, err := upgrader.Upgrade(response, request, nil)
    if err != nil {
        fmt.Println(err)
        return
    }
    msg_type, msg := readIncommingCommand(conn)
    sendCommand(conn, msg_type, msg)
}

func readIncommingCommand(conn *websocket.Conn) (msg_type int, msg []byte){
    msg_type, msg, err := conn.ReadMessage()
    if err != nil {
        fmt.Println(err)
    }
    fmt.Printf("Received Msg: " + string(msg[:]))
    var cmd models.Command
    err = json.Unmarshal(msg, &cmd)
    fmt.Printf("Received Cmd: " + cmd.Id + " msg: " + cmd.Data)
    return msg_type, msg
}

func sendCommand(conn *websocket.Conn, msg_type int, msg []byte){
    var write_err error = nil
    if write_err = conn.WriteMessage(msg_type, msg)
    write_err != nil {
        fmt.Println("Send error")
        return
    }
}
