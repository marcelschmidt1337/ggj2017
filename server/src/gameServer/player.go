package gameServer

import "github.com/gorilla/websocket"
import "time"
import "fmt"

// Inheritance in go: http://golangtutorials.blogspot.de/2011/06/inheritance-and-subclassing-in-go-or.html
type Player struct{
    BaseClient
    IsActive bool
    ActiveRowStartTime time.Time
}

func NewPlayer(conn *websocket.Conn) (player Player){
    player = Player{NewClient(conn), false, time.Now().UTC()}
    return player
}

func (player Player) StartRow(){
    player.ActiveRowStartTime = time.Now().UTC()
    fmt.Println("Player starts rowing at time " + player.ActiveRowStartTime.String())
}
func (player Player) StopRow(){
    var duration = time.Since(player.ActiveRowStartTime)
    fmt.Println("Player finished rowing in time: " + duration.String())
}
