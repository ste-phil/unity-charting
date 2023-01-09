/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Contributors:    https://github.com/Tayx94/graphy/graphs/contributors
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            15-Dec-17
 * Studio:          Tayx
 *
 * Git repo:        https://github.com/Tayx94/graphy
 *
 * This project is released under the MIT license.
 * Attribution is not required, but it is always welcomed!
 * -------------------------------------*/

using Modules.Charting.Entities.Monitors;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Charting.Entities
{
    public class Graph : GraphBase
    {
        [SerializeField] private Image  m_imageGraph    = null;
        [SerializeField] private Shader ShaderFull      = null;
        [SerializeField] private Shader ShaderLight     = null;

        // This keeps track of whether Init() has run or not
        [SerializeField] private bool m_isInitialized   = false;

		private	ChartingSystem	m_chartManager;
        private	MonitorSample   m_sampleMonitor		    = null;
        private GraphShader     m_shaderGraph		    = null;

        private void Init()
        {
            m_chartManager = transform.root.GetComponentInChildren<ChartingSystem>();
            m_sampleMonitor = GetComponent<MonitorSample>();
            m_shaderGraph = new GraphShader
            {
                Image = m_imageGraph
            };

            UpdateParameters();

            m_isInitialized = true;
        }

        private void Update()
        {
            UpdateGraph();
        }
        
        #region Methods -> Public
        public void UpdateParameters()
        {
			m_shaderGraph.ArrayMaxSize = GraphShader.ArrayMaxSizeLight;
			m_shaderGraph.Image.material = new Material(ShaderLight);

            m_shaderGraph.InitializeShader();
            
            CreatePoints();
        }
        #endregion

        #region Methods -> Protected Override

        protected override void UpdateGraph()
        {
            // Since we no longer initialize by default OnEnable(), 
            // we need to check here, and Init() if needed
            if (!m_isInitialized)
            {
                Init();
            }
            

            if (m_shaderGraph.ShaderArrayValues == null || m_shaderGraph.ShaderArrayValues.Length != m_sampleMonitor.SampleCapacity)
            {
                m_shaderGraph.ShaderArrayValues         = new float[m_sampleMonitor.SampleCapacity];
            }

			var cIdx = m_sampleMonitor.CurrentSampleIdx;
            for (int i = 0; i < m_sampleMonitor.SampleCapacity; i++)
            {
                m_shaderGraph.ShaderArrayValues[i]      = m_sampleMonitor.Samples[(cIdx + i) % m_sampleMonitor.SampleCapacity] / (float) m_sampleMonitor.Maximum;
            }

            // Update the material values
            m_shaderGraph.UpdatePoints();

            m_shaderGraph.Average           = m_sampleMonitor.Average / (float)m_sampleMonitor.Maximum;
            m_shaderGraph.UpdateAverage();

            m_shaderGraph.GoodThreshold     = (float) m_chartManager.GoodThreshold / (float)m_sampleMonitor.Maximum;
            m_shaderGraph.CautionThreshold  = (float) m_chartManager.CautionThreshold / (float)m_sampleMonitor.Maximum;
            m_shaderGraph.UpdateThresholds();
        }

        protected override void CreatePoints()
        {
            if (m_shaderGraph.ShaderArrayValues == null || m_shaderGraph.ShaderArrayValues.Length != m_sampleMonitor.SampleCapacity)
            {
                m_shaderGraph.ShaderArrayValues     = new float[m_sampleMonitor.SampleCapacity];
            }

            for (int i = 0; i < m_sampleMonitor.SampleCapacity; i++)
            {
                m_shaderGraph.ShaderArrayValues[i] = 0;
            }

            m_shaderGraph.GoodColor     = m_chartManager.GoodColor;
            m_shaderGraph.CautionColor  = m_chartManager.CautionColor;
            m_shaderGraph.CriticalColor = m_chartManager.CriticalColor;
            
            m_shaderGraph.UpdateColors();
            m_shaderGraph.UpdateArray();
        }
        #endregion
    }
}