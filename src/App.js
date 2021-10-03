import './App.css';
import React, {useState} from "react";
import {GameMenu} from "./GameMenu";
import {Modes} from "./constants";
import {Game} from "./Game";

const App = () => {
    const [ showNewGameMenu, setShowNewGameMenu ] = useState(true);

    // Game state
    const [ isGameOn, setIsGameOn ] = useState(false);
    const [ mode, setMode ] = useState(Modes.WithComputer);

    const onStart = (mode) => {
        console.log(mode);
        setMode(mode);
        setIsGameOn(true);
    };

    return (
        <div className="App">
            <header className="App-header">
                Quoridor
            </header>

            {!isGameOn && <button className="btn primary" onClick={() => setShowNewGameMenu(true)}>New game</button>}
            {isGameOn && <Game mode={mode}/>}

            {showNewGameMenu && <GameMenu onStart={onStart}/>}
        </div>
    );
};

export default App;
