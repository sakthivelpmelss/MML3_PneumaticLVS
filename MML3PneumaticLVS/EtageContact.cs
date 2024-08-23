using MML3PneumaticLVS.CustomDataType;

namespace MML3PneumaticLVS
{
    public class EtageContact : EtContact
    {
        private bool _bInverse;
        private bool _bInverseConsigne;
        private bool _bInverseNO;
        private bool _bInverseNC;
        private bool _bInverseCOMMUN;

        public EtageContact(bool bInverse, int nPatteInverse)
        {
            _bInverse = bInverse;
            _bInverseConsigne = bInverse;
            _bInverseNO = (1 & nPatteInverse) != 0;
            _bInverseNC = (2 & nPatteInverse) != 0;
            _bInverseCOMMUN = (4 & nPatteInverse) != 0;
        }

        public override void Step(Signal sNO, Signal sNC, Signal sCOMMUN, Signal sCOMMANDE)
        {
            int nNO = sNO.Etat & 1; 
            int nNC = sNC.Etat & 1;
            int nCOMMUN = sCOMMUN.Etat & 1;

            if (_bInverseConsigne)
            {
                if (sCOMMANDE.Etat == 0)
                {
                    if (sCOMMUN.Etat > 0 && sNC.Etat < 1)
                    {
                        if (sNC.VS_handle != 0 || sNC.VS_handleIn != 0)
                            _bInverse = true;
                    }
                    else
                        _bInverse = false;
                }
                else
                {
                    if (sCOMMUN.Etat > 0 && sNO.Etat < 1)
                    {
                        if (sNO.VS_handle != 0 || sNO.VS_handleIn != 0)
                            _bInverse = true;
                    }
                    else
                        _bInverse = false;
                }
            }
            else
            {
                if (sCOMMANDE.Etat > 0)
                {
                    if (sCOMMUN.Etat > 0 && sNO.Etat < 1)
                    {
                        if (sNO.VS_handle != 0 || sNO.VS_handleIn != 0)
                            _bInverse = true;
                    }
                    else
                        _bInverse = false;
                }
                else
                {
                    if (sCOMMUN.Etat > 0 && sNC.Etat < 1)
                    {
                        if (sNC.VS_handle != 0 || sNC.VS_handleIn != 0)
                            _bInverse = true;
                    }
                    else
                        _bInverse = false;
                }
            }

            if (!_bInverse)
            {
                if (_bInverseNO || _bInverseCOMMUN)
                    nNO = ~nNO;
                if (_bInverseNC || _bInverseCOMMUN)
                    nNC = ~nNC;

                if (sCOMMANDE.Etat > 0)
                    sCOMMUN.Etat = sCOMMUN.Etat | nNO;
                else
                    sCOMMUN.Etat = sCOMMUN.Etat | nNC;
            }
            else
            {
                if (sCOMMANDE.Etat > 0)
                {
                    if (_bInverseNO || _bInverseCOMMUN)
                        nCOMMUN = ~nCOMMUN;
                    if (_bInverseNC || _bInverseCOMMUN)
                        nNC = 1;
                    else
                        nNC = 0;

                    sNO.Etat = sNO.Etat | nCOMMUN;
                    sNC.Etat = nNC;
                }
                else
                {
                    if (_bInverseNC || _bInverseCOMMUN)
                        nCOMMUN = ~nCOMMUN;
                    if (_bInverseNO || _bInverseCOMMUN)
                        nNO = 1;
                    else
                        nNO = 0;

                    sNC.Etat = sNC.Etat | nCOMMUN;
                    sNO.Etat = nNO;
                }
            }
        }
    }
}
