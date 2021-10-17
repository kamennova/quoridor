import React, {useState} from "react";
import {Modes} from "./constants";



export const GameMenu = ({onStart}) => {
    
    const [ mode, setMode ] = useState(Modes.TwoPlayers);
    
    return (
        <div className="modal" >
            <h3>New game</h3>
            <div className="content">
            <h4>Mode</h4>
            <label>
                <input type="radio" id="mode" value="two-players" checked={mode === Modes.TwoPlayers}
                       onClick={() => setMode(Modes.TwoPlayers)}/>
                Two players
            </label>

            <label>
                <input type="radio" id="mode" value="computer" checked={mode === Modes.WithComputer}
                       onClick={() => setMode(Modes.WithComputer)}/>
                Against computer
            </label>
            </div>

            <div className="controls">
                <div className="btn">Cancel</div>
                <div className="btn primary" onClick={() => onStart(mode)}>Start</div>
            </div>
            
        </div>
    );
};
