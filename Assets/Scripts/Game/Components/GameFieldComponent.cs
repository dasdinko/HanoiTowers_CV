using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HanoiTowers.Scripts.Game.Components
{
    public class GameFieldComponent : MonoBehaviour
    {
        [SerializeField] Transform[] _poleHolders;
        [SerializeField] DiscComponent _diskPrefab;

        public Action TaskComplete;
        
        Pole[] _poles;
        DiscComponent[] _disks;

        // settings stuff
        int _discsCount;
        int _polesCount = 3;

        float _discHeigh = 0.2f;
        float _discWith = 1.5f;
        float _yPickPosition = 6.5f;



        public void Init(int discsCount)
        {
            _discsCount = discsCount;
            _poles = new Pole[_polesCount];
            _disks = new DiscComponent[_discsCount];

            for (int i = 0; i < _polesCount; i++)
            {
                _poles[i] = new Pole(i, _poleHolders[i].position, new Stack<DiscComponent>(_discsCount));
            }
            
            for (int i = 0; i < _discsCount; i++)
            {
                var disc = Instantiate(_diskPrefab, new Vector3(_poles[0].Position.x, i * _discHeigh),
                    Quaternion.identity, this.transform);
                
                disc.Init(_discWith - _discHeigh * i, HanoiTowersConsts.DISC_COLORS[i]);

                _disks[i] = disc;
            }

            for (int i = 0; i < _disks.Length; i++)
            {
                _poles[0].Push(_disks[i]);
            }
        }

        public void CompleteHanoiTowersTask()
        {
            StartCoroutine(Move(_discsCount, _poles[0], _poles[2], _poles[1]));
        }
        
        
        IEnumerator Move( int discs, Pole fromPole, Pole toPole, Pole otherPole, float onePoleFlyTime = 2f)
        {
            
            yield return new WaitForSeconds(0.2f); // Pause time before pick
            
            float time = onePoleFlyTime * Mathf.Abs(fromPole.Index - toPole.Index);
            
            if (discs == 1)
            {
                DiscComponent disk = fromPole.Pop();
                toPole.Push(disk);
                
                disk.Animate(_yPickPosition, new Vector3(toPole.Position.x, (toPole.Count - 1) * _discHeigh), time);
                
                yield return new WaitForSeconds(time);

                CheckForWin();
                
                yield break;
            }

            yield return StartCoroutine(Move(discs - 1, fromPole, otherPole, toPole));
            
            DiscComponent anotherDisk = fromPole.Pop();

            toPole.Push(anotherDisk);

            anotherDisk.Animate(_yPickPosition, new Vector3(toPole.Position.x, (toPole.Count - 1) * _discHeigh), time);
            
            yield return new WaitForSeconds(time);

            yield return StartCoroutine(Move(discs - 1, otherPole, toPole, fromPole));
            
        }

        void CheckForWin()
        {
            var lastPole = _poles[_polesCount - 1];
            
            if(lastPole.GetDisksCountOnPole() == _discsCount)
                TaskComplete.Invoke();
        }

    }
    
    public struct Pole
    {
        public readonly int Index;
        
        public Vector3 Position;

        public int Count
        {
            get { return _disks.Count; }
        }

        Stack<DiscComponent> _disks;

        public Pole(int index, Vector3 position, Stack<DiscComponent> disks)
        {
            Index = index;
            Position = position;
            _disks = disks;
        }

        public DiscComponent Pop()
        {
            return _disks.Pop();
        }

        public void Push(DiscComponent disk)
        {
            _disks.Push(disk);
        }

        public int GetDisksCountOnPole()
        {
            return _disks.Where(d => d != null).ToArray().Length;
        }
    }
}