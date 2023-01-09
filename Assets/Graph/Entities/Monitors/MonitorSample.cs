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
using System;
using UnityEngine;

namespace Modules.Charting.Entities.Monitors
{
	public class MonitorSample : MonoBehaviour
	{
		private short[] m_samples;
		private short m_samplesCapacity = 128;
		private short m_indexSample = 0;
		private int m_sum;

		public short Value => m_samples[Math.Max(CurrentSampleIdx - 1, 0)];
		public short Maximum { get; private set; }
		public short Average { get; private set; }
		public short OnePercent { get; private set; } = 0;
		public short Zero1Percent { get; private set; } = 0;

		public short CurrentSampleIdx => (short)(m_indexSample);
		public short[] Samples => m_samples;
		public int SampleCapacity => m_samplesCapacity;

		private void Awake()
		{
			Init();
		}

		public void AddSample(short value)
		{
			var oldValue = m_samples[m_indexSample];
			m_samples[m_indexSample] = value;
			m_indexSample = (short)((m_indexSample + 1) % m_samplesCapacity);

			m_sum =  m_sum + value - oldValue;

			Maximum = Math.Max(Maximum, value);
			Average = (short)(m_sum / (float) m_samplesCapacity);
		}


		private void Init()
		{
			m_samples = new short[m_samplesCapacity];
			//m_samplesSorted = new short[m_samplesCapacity];

			UpdateParameters();
		}

		public void UpdateParameters()
		{
			//m_onePercentSamples = (short)(m_samplesCapacity / 100);
			//m_zero1PercentSamples = (short)(m_samplesCapacity / 1000);
		}

	}
}
