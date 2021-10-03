const makeReq = (method, content) => {
    // todo request
};

export const tryMove = async (moveType, coordinate, playerId) => {
    return {
        body: {
            isValid: true,
            isFinished: false,
        }
    };
};

export const getOpponentMove = async () => {
    return {
        body: {
            coord: [],
            isFinished: false,
        }
    };
};

export const finishGame = async () => {

};

export const startGame = async (mode) => {
    return {
        body: {
            coordinates: {
                0: [ 2, 0 ],
                1: [ 1, 0 ], // random
            },
        }
    }
};
