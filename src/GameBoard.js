import React from "react";

export const GameBoard = ({horizontalWalls, verticalWalls, players, onGoToCell}) => {

    return (
        <div className="board">
            {
                [ ...Array(9) ].map(
                    (elem, y) => <div class={"cell-row "}>{[ ...Array(9) ].map((elem2, x) => <Cell onClick={() => onGoToCell(y * 9 + x)}/>)}</div>)
            }
            <PlayerMark id={0} coord={players[0]} />
            <PlayerMark id={1} coord={players[1]} />
        </div>
    );
};

const Cell = ({isActive, onClick}) => (
    <div className={"cell " + (isActive ? "" : "free")} onClick={onClick}>

    </div>
);

const PlayerMark = ({coord, id}) => (
    <div className={"player-mark " + (id === 0 ? "player-1" : "player-2")}
         style={{left: coord[ 0 ] * 62, bottom: coord[ 1 ] * 62}}/>
);
