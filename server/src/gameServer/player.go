package gameServer

import "github.com/gorilla/websocket"

// Inheritance in go: http://golangtutorials.blogspot.de/2011/06/inheritance-and-subclassing-in-go-or.html
type Player struct{
    BaseClient
}

func NewPlayer(conn *websocket.Conn) (player Player){
    player = Player{NewClient(conn)}
    return player
}
