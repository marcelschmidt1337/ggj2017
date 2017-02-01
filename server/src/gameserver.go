package main

import "net/http"
import "fmt"
import "github.com/gorilla/websocket"

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
    msg_type, msg, read_err := conn.ReadMessage()
    if read_err != nil {
        fmt.Println(read_err)
    }
    fmt.Printf("Received Msg: " + string(msg[:]))
    if read_err = conn.WriteMessage(msg_type, msg);
    read_err != nil {
        fmt.Println("Send error")
        return
    }
}
