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
var registeredPlayer map[int]*gameServer.Player
var registeredViewer map[int]*gameServer.Viewer

func main(){
    registeredPlayers = make(map[int]*gameServer.Player)
    fmt.Printf("Server is running\n")
    http.HandleFunc("/", Serve)
    http.ListenAndServe(":7777", nil)
}

func Serve(response http.ResponseWriter, request *http.Request){
    conn, err := upgrader.Upgrade(response, request, nil)
    if err != nil {
        fmt.Println(err)
        return
    }
    msg_type, msg := ReadIncommingCommands(conn)
    sendCommand(conn, msg_type, msg)
}

func ReadIncommingCommands(conn *websocket.Conn) (msg_type int, msg []byte){
    msg_type, msg, err := conn.ReadMessage()
    if err != nil {
        fmt.Println(err)
    }
    fmt.Println("Received Msg: " + string(msg[:]))
    cmd := models.NewCommandFromJson(msg)
    id := strconv.Itoa(cmd.Id)
    fmt.Println("Received Cmd: " + id + " msg: " + cmd.Data)

    switch cmd.Id {
        case models.RegisterPlayer:
            RegisterPlayer(conn)
            BroadCastToAllPlayers()
        case models.RegisterViewer:
            RegisterViewer(conn)
    }

    return msg_type, msg
}

func RegisterPlayer(conn *websocket.Conn){
    player := gameServer.NewPlayer(conn)
    registeredPlayers[highestClientId] = player
    IncreaseClientId()
}

func RegisterViewer(conn *websocket.Conn){
    viewer := gameServer.NewViewer(conn)
    registeredViewer[highestClientId] = viewer
    IncreaseClientId()
}

func IncreaseClientId(){
    highestClientId++
}

func BroadCastToAllPlayers(){
    BroadCast("Hello Player", registeredPlayers)
}

func BroadCastToAllViewers(){
    BroadCast("Hello Viewer", registeredViewer);
}
func BroadCastToAllViewers(msg string, receiverList map[]*gameServer.Client){
    dummyCommand := models.NewCommand(models.HelloClient, msg)
    for _, client := range receiverList {
        client.SendCommand(dummyCommand)
    }
}
