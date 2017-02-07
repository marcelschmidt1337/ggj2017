package gameServer

import "github.com/gorilla/websocket"

type Viewer struct{
    BaseClient
}

func NewViewer(conn *websocket.Conn) (viewer *Viewer){
    viewer = new(Viewer{NewClient(conn)})
    return viewer
}
