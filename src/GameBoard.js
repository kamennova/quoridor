import React from "react";

export const GameBoard = ({horizontalWalls, verticalWalls, players, onGoToCell, onPutWall}) => {

    return (
        <div className="board">
            <div className="board-cells">
                    {
                        [ ...Array(9) ].map(
                            (elem, y) => <div class={"cell-row "}>{[ ...Array(9) ].map((elem2, x) =>
                                <Cell onClick={() => onGoToCell({x, y})}/>)}</div>)
                    }
                    <PlayerMark id={0} coord={players[0]} />
                    <PlayerMark id={1} coord={players[1]} />
            </div>
            <div className="board-walls horizontal">
                {
                    [ ...Array(8) ].map(
                        (elem, y) => <div
                            class={"wall-row horizontal"}>{[ ...Array(9) ].map((elem2, x) => <WallHorizontal onClick={() => onPutWall({x, y}, "horizontal")} />)}</div>)
                }
            </div>
            <div className="board-walls vertical">
                {
                    [ ...Array(8) ].map(
                        (elem, y) => <div class={"wall-row vertical"}>{[ ...Array(9) ].map((elem2, x) => <WallVertical onClick={() => onPutWall({x, y: 8-y}, "vertical")} />)}</div>)
                }
            </div>
        </div>

    );
    };

const Cell = ({isActive, onClick}) => (
    <div className={"cell " + (isActive ? "" : "free")} onClick={onClick}>

    </div>
);

const WallHorizontal = ({isActive, onClick}) => (
    <div className={"wall horizontal " + (isActive ? "" : "free")} onClick={onClick}>

    </div>
);

const WallVertical = ({isActive, onClick}) => (
    <div className={"wall vertical " + (isActive ? "" : "free")} onClick={onClick}>

    </div>
);

const PlayerMark = ({coord, id}) => (
    <div className={"player-mark " + (id === 0 ? "player-1" : "player-2")}
         style={{left: coord[ 0 ] * 62, bottom: coord[ 1 ] * 62}}/>
);
