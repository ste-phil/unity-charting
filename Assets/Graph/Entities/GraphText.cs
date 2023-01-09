/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Contributors:    https://github.com/Tayx94/graphy/graphs/contributors
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            22-Nov-17
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

namespace Modules.Charting
{
    public class GraphText : MonoBehaviour
    {
		#region Variables -> Private
		[SerializeField] private	Text			    m_chartName				= null;
		[SerializeField] private    Text                m_valueText             = null;
        [SerializeField] private    Text                m_avgValueText          = null;
        [SerializeField] private    Text                m_onePercentText        = null;
        [SerializeField] private    Text                m_zero1PercentText      = null;

        private                     ChartingSystem		m_graphyManager			= null;
        private						MonitorSample		m_sampleMonitor			= null;
        private                     int					m_updateRate			= 4;  // 4 updates per sec.
        private                     float				m_deltaTime				= 0f;
        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            m_graphyManager = transform.root.GetComponentInChildren<ChartingSystem>();

            m_sampleMonitor = GetComponent<MonitorSample>();

            UpdateParameters();
        }

        private void Update()
        {
            m_deltaTime += Time.unscaledDeltaTime;

            // Only update texts 'm_updateRate' times per second
            if (m_deltaTime > 1f / m_updateRate)
            {
				// Update fps
				var value = m_sampleMonitor.Value;
				m_valueText.text = Mathf.RoundToInt(value).ToString();
                SetColorCodedTextColor(m_valueText, value);

                // Update 1% fps
                m_onePercentText.text = ((int)(m_sampleMonitor.OnePercent)).ToString();
                SetColorCodedTextColor(m_onePercentText, m_sampleMonitor.OnePercent);

                // Update 0.1% fps
                m_zero1PercentText.text = ((int)(m_sampleMonitor.Zero1Percent)).ToString();
                SetColorCodedTextColor(m_zero1PercentText, m_sampleMonitor.Zero1Percent);

                // Update avg fps
                m_avgValueText.text = ((int)(m_sampleMonitor.Average)).ToString();
                SetColorCodedTextColor(m_avgValueText, m_sampleMonitor.Average);

                // Reset variables
                m_deltaTime = 0f;
            }
        }
        
        public void UpdateParameters()
        {
			m_chartName.text = m_graphyManager.ChartName;
		}


        /// <summary>
        /// Assigns color to a text according to their fps numeric value and
        /// the colors specified in the 3 categories (Good, Caution, Critical).
        /// </summary>
        /// 
        /// <param name="text">
        /// UI Text component to change its color
        /// </param>
        /// 
        /// <param name="fps">
        /// Numeric fps value
        /// </param>
        private void SetColorCodedTextColor(Text text, float fps)
        {
			if (fps > m_graphyManager.GoodThreshold)
				text.color = m_graphyManager.GoodColor;
			else if (fps > m_graphyManager.CautionThreshold)
				text.color = m_graphyManager.CautionColor;
			else
				text.color = m_graphyManager.CriticalColor;
		}
    }
}
