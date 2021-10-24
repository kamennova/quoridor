const BASE_URL = "https://localhost:5001/api/Game";

/**
 *  {
        body: {
            isValid: true,
            isGameFinished: false,
        }
    };
 */
export const tryMove = async (moveType, coordinate, playerId) => fetch(BASE_URL + "/try-move", {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
    },
    body: {
        moveType,
        coordinate,
        playerId
    }
}).then(res => res.json());

/**
 * {
        body: {
            coord: [],
            isFinished: false,
        }
    };
 */
export const getOpponentMove = async () => fetch(BASE_URL + "/opponentMove", {
    method: "GET",
    headers: {"Content-Type": "application/json",}
});


export const finishGame = async () => fetch(BASE_URL + "/finish", {method: "POST"});


/**
 * {
        body: {
            coordinates: {
                0: [ 4, 0 ],
                1: [ 4, 8 ], // not random, but standart start position
            },
        }
    }
 */

export const startGame = async (mode) => fetch(BASE_URL + "/start", {
    method: "POST",
    body: {
        mode,
    },
    headers:{
        "Content-Type": "application/json"
    }
}).then(res => res.json());
