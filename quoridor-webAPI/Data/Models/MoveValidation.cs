using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class MoveValidationResult
    {

        public bool isValid;
        public bool isGameFinished;
        public int winnerId;
        public string moveError;

       public MoveValidationResult(bool isValid, bool isGameFinished){
        this.isValid = isValid;
        this.isGameFinished = isGameFinished;
       }

       public void setWinnerId(int winnerId) {
            this.winnerId = winnerId;
       }

       public void setMoveError(string error) {
           this.moveError = error;
       }

    }
}