const BASE_URL = "localhost:5000";

// [0, 2], {x: 0, y: 2}, 76
/**
 *  {
        body: {
            isValid: true,
            isGameFinished: false,
        }
    };
 */
export const tryMove = async (moveType, coordinate, playerId) => fetch(BASE_URL + "/tryMove", {
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
                0: [ 2, 0 ],
                1: [ 1, 0 ], // random
            },
        }
    }
 */

export const startGame = async (mode) => fetch(BASE_URL + "/start?mode=" + mode, {
    method: "POST",
});
