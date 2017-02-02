package main

import "net/http"
import "fmt"
import "github.com/gorilla/websocket"

import "gameServer/models"
import "gameServer"
import "strconv"

var upgrader = websocket.Upgrader{
    CheckOrigin: func(r *http.Request) bool {
        return true
    },
}

var highestClientId int = 0
var registeredClients map[int]*gameServer.Client

func main(){
    registeredClients = make(map[int]*gameServer.Client)
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
    fmt.Println("Received Msg: " + string(msg[:]))
    cmd := models.NewCommandFromJson(msg)
    id := strconv.Itoa(cmd.Id)
    fmt.Println("Received Cmd: " + id + " msg: " + cmd.Data)

    switch cmd.Id {
    case models.RegisterClient:
        registerClient(conn)
        BroadCastToAllClients()
    }

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

func registerClient(conn *websocket.Conn){
    client := gameServer.NewClient(conn)
    registeredClients[highestClientId] = client
    highestClientId++
}

func BroadCastToAllClients(){
    fmt.Println("Broadcast welcome command")
    dummyCommand := models.NewCommand(models.HelloClient, "Welcome on server")
    for _, client := range registeredClients {
        client.SendCommand(dummyCommand)
    }
}
