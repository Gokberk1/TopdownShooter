using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TMP_Text _coinText;
    [SerializeField] GameObject _animatedCoinPrefab;
    [SerializeField] Transform _target;

    [Space]
    [Header("Available coins: (Coins to pool)")]
    [SerializeField] int _maxCoin;
    Queue<GameObject> _coinsQueue = new Queue<GameObject>();

    [Space]
    [Header ("Animation Settings")]
    [SerializeField] [Range(0.1f, 1f)] float _minAnimDuration;
    [SerializeField] [Range(1f, 2f)] float _maxAnimDuration;

    [SerializeField] Ease _easeType;
    [SerializeField] float _spread;

    Vector3 _targetPosition;

    private int _c;
    public int Coins { get { return _c; } set { _c = value; _coinText.text = Coins.ToString(); } }

    private void Awake()
    {
        _targetPosition = _target.position;
        PrepareCoins();
    }

    void PrepareCoins()
    {
        GameObject coin;

        for (int i = 0; i < _maxCoin; i++)
        {
            coin = Instantiate(_animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            _coinsQueue.Enqueue(coin);
        }
    }

    void Animate(Vector3 collectedCoinPos, int amount)
    {
        Coins += amount;
        for (int i = 0; i < amount; i++)
        {
            if(_coinsQueue.Count > 0)
            {
                GameObject coin = _coinsQueue.Dequeue();
                coin.SetActive(true);

                coin.transform.position = collectedCoinPos + new Vector3(Random.Range(-_spread, _spread), 0f, 0f);

                float duration = Random.Range(_minAnimDuration, _maxAnimDuration);
                coin.transform.DOMove(_targetPosition, duration)
                .SetEase(_easeType)
                .OnComplete(() =>
                {
                    coin.SetActive(false);
                    _coinsQueue.Enqueue(coin);
                });
            }
        }
    }

    public void AddCoin(Vector3 collectedCoinPos, int amount)
    {
        Animate(collectedCoinPos, amount);
    }
}
