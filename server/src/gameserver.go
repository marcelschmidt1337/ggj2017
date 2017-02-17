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
var registeredPlayer map[*websocket.Conn]gameServer.Player
var registeredViewer map[*websocket.Conn]gameServer.Viewer

func main(){
    registeredPlayer = make(map[*websocket.Conn]gameServer.Player)
    registeredViewer = make(map[*websocket.Conn]gameServer.Viewer)
    fmt.Printf("Server is running\n")
    http.HandleFunc("/", Serve)
    http.ListenAndServe(":7777", nil)
}

func Serve(response http.ResponseWriter, request *http.Request){
    fmt.Println("Incoming!")
    conn, err := upgrader.Upgrade(response, request, nil)
    if err != nil {
        fmt.Println(err)
        return
    }
    ReadIncommingCommands(conn)
}

func ReadIncommingCommands(conn *websocket.Conn) (msg_type int, msg []byte){
    msg_type, msg, err := conn.ReadMessage()
    if err != nil {
        fmt.Println(err)
    }
    fmt.Println("Received Msg: " + string(msg[:]))
    cmd := models.NewCommandFromJson(msg)
    id := strconv.Itoa(cmd.CommandId)
    fmt.Println("Received Cmd: " + id)

    switch cmd.CommandId {
        case models.RegisterPlayer:
            RegisterPlayer(conn)
            BroadCastToAllPlayers()
        case models.RegisterViewer:
            RegisterViewer(conn)
        case models.PlayerStartRow:
            registeredPlayer[conn].StartRow()
        case models.PlayerStopRow:
            registeredPlayer[conn].StopRow()
    }

    return msg_type, msg
}

func RegisterPlayer(conn *websocket.Conn){
    player := gameServer.NewPlayer(conn)
    registeredPlayer[conn] = player
}

func RegisterViewer(conn *websocket.Conn){
    viewer := gameServer.NewViewer(conn)
    registeredViewer[conn] = viewer
}

func BroadCastToAllPlayers(){
    BroadCastP("Hello Player", registeredPlayer)
}

func BroadCastToAllViewers(){
    BroadCastV("Hello Viewer", registeredViewer)
}
func BroadCastP(msg string, receiverList map[*websocket.Conn]gameServer.Player){
    dummyCommand := models.NewCommand(models.HelloClient)
    for _, client := range receiverList {
        client.SendCommand(dummyCommand)
    }
}
func BroadCastV(msg string, receiverList map[*websocket.Conn]gameServer.Viewer){
    dummyCommand := models.NewCommand(models.HelloClient)
    for _, client := range receiverList {
        client.SendCommand(dummyCommand)
    }
}
func BroadCast(msg string, receiverList map[*websocket.Conn]gameServer.Client){
    dummyCommand := models.NewCommand(models.HelloClient)
    for _, client := range receiverList {
        client.SendCommand(dummyCommand)
    }
}
