package models

import "encoding/json"
import "fmt"
import "log"

const (
    RegisterPlayer int = iota
    RegisterViewer int = iota
    PlayerStartRow int = iota
    PlayerStopRow int = iota
    HelloClient int = iota
)

type Command struct{
    CommandId int
}

func (cmd Command) GetRawData() (data []byte){
    data, err := json.Marshal(cmd)
    if err != nil {
        fmt.Println("Error reading Command RawData")
        log.Println(err)
    }
    return data
}

func (cmd Command) ToJsonString() (jsonData string){
    return string(cmd.GetRawData()[:])
}

func NewCommand(commandId int) (cmd Command){
    cmd.CommandId = commandId
    return cmd
}
func NewCommandFromJson(jsonData []byte) (cmd *Command){
    err := json.Unmarshal(jsonData, &cmd)
    if err != nil {
        fmt.Println("Error creating new Command: ")
        log.Println(err)
    }
    return cmd
}
