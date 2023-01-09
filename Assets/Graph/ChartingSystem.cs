using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Modules.Charting.Entities.Monitors;
using Modules.Charting.Entities;

namespace Modules.Charting
{
    public class ChartingSystem : MonoBehaviour
    {
        [SerializeField] private    GameObject                  m_fpsGraphGameObject        = null;

		[SerializeField] private Color m_goodColor = new Color32(118, 212, 58, 255);
		[SerializeField] private int m_goodThreshold = 60;

		[SerializeField] private Color m_cautionColor = new Color32(243, 232, 0, 255);
		[SerializeField] private int m_cautionThreshold = 30;

		[SerializeField] private Color m_criticalColor = new Color32(220, 41, 30, 255);
		[SerializeField] private string m_chartName = "Samples";

		private Graph m_graph = null;
        private GraphText m_text = null;
        private MonitorSample m_monitor = null;
		private RectTransform m_rectTransform = null;
		private List<GameObject> m_childrenGameObjects = new List<GameObject>();

		public string ChartName => m_chartName;
		public Color GoodColor => m_goodColor;
		public Color CautionColor => m_cautionColor;
		public Color CriticalColor => m_criticalColor;
		public int GoodThreshold => m_goodThreshold;
		public int CautionThreshold => m_cautionThreshold;

		
        
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            m_rectTransform = GetComponent<RectTransform>();

            m_graph = GetComponent<Graph>();
            m_monitor = GetComponent<MonitorSample>();
            m_text = GetComponent<GraphText>();

            foreach (Transform child in transform)
            {
                if (child.parent == transform)
                {
                    m_childrenGameObjects.Add(child.gameObject);
                }
            }
        }

        public void UpdateParameters()
        {
            m_graph      .UpdateParameters();
            m_monitor    .UpdateParameters();
            m_text       .UpdateParameters();
        }

        public void RefreshParameters()
        {
            m_graph      .UpdateParameters();
            m_monitor    .UpdateParameters();
            m_text       .UpdateParameters();
        }
    }
}