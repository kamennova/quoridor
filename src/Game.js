import React, {useEffect, useState} from "react";
import {GameBoard} from "./GameBoard";
import {startGame, tryMove} from "./requests";
import {Modes, MoveTypes} from "./constants";

export const Game = ({mode}) => {
    const [ wrongMove, setWrongMove ] = useState(false);
    const [ madeMove, setMadeMove ] = useState(false);
    const [ turn, setTurn ] = useState(0);
    const [ playersCoords, setPlayersCoords ] = useState({0: [], 1: []});
    const [ horizontalWalls, setHorizontalWalls ] = useState([]);
    const [ verticalWalls, setVerticalWalls ] = useState([]); // [ [0, 2], [1, 5]   ]

    const clearGame = () => {
        // todo
    };

    useEffect(() => {
        startGame(mode).then((res) => {
            clearGame();
            setPlayersCoords({...res.body.coordinates});
        });
    }, []);


    useEffect(() => {
        if (madeMove) {
            console.log("Made move");
            nextPlayersTurn();
        }
    }, [ madeMove ]);

    const nextPlayersTurn = () => {
        setMadeMove(false);
        setTurn(turn === 0 ? 1 : 0);
    };

    const makeMove = (moveType, coordinate) => {
        console.log(moveType, coordinate);

        tryMove(moveType, coordinate).then((res) => {
            if (res.body.isValid) {
                setMadeMove(true);
            } else {
                // todo tell user that move is wrong
            }
        });
    };
    // todo highlight marker that should make move

    return (
        <div>
            {/* todo finish game btn*/}
            <span className={"turn-message"}>
            {turn === 0 && (mode === Modes.WithComputer ? "Your" : "Player 1'") + " turn"}
                {turn === 1 && "Player 2' turn"}
            </span>

            {wrongMove && <span>Invalid wrong</span>}
            <GameBoard onGoToCell={(id) => makeMove(MoveTypes.GoToCell, id)}
                       onPutWall={(coord) => makeMove(MoveTypes.PutWall, coord)}
                       horizontalWalls={horizontalWalls}
                       verticalWalls={verticalWalls}
                       players={playersCoords}/>
        </div>
    );
};

