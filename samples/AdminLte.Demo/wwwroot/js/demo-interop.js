// AdminLTE demo plugin interop for the Blazor (InteractiveServer) sample.
// Each function initialises a third-party plugin against a DOM element passed
// from a Blazor page (via @ref ElementReference) in OnAfterRenderAsync.
// The Chart.js configs are copied from the core AdminLTE dist demo pages
// (index.html / index2.html / index3.html); the theme preset is chart-theme.js.
window.adminlteDemo = {
  // ---- Dashboard v1 (index.html) -----------------------------------------

  // Sales Value area chart.
  salesAreaChart(el) {
    if (!el || typeof Chart === 'undefined') return;
    areaChart(el);
  },

  // World map (jsvectormap) + the three footer sparklines (#sparkline-1/2/3).
  worldMap(el) {
    if (!el || typeof jsVectorMap === 'undefined') return;

    new jsVectorMap({
      selector: el,
      map: 'world',
    });

    if (typeof Chart === 'undefined') return;
    const sparkline = (selector, data) => {
      const canvas = document.querySelector(selector);
      if (!canvas) return;
      new Chart(canvas, {
        type: 'line',
        data: {
          labels: data.map((value, index) => index + 1),
          datasets: [
            {
              data,
              borderColor: '#DCE6EC',
              backgroundColor: 'rgba(220, 230, 236, 0.3)',
              pointBackgroundColor: '#DCE6EC',
              fill: true,
            },
          ],
        },
        options: {
          layout: { padding: { top: 2 } },
          plugins: {
            legend: { display: false },
            tooltip: { displayColors: false, callbacks: { title: () => '' } },
          },
          scales: {
            x: { display: false },
            y: { display: false, min: 0 },
          },
        },
      });
    };

    sparkline('#sparkline-1', [1000, 1200, 920, 927, 931, 1027, 819, 930, 1021]);
    sparkline('#sparkline-2', [515, 519, 520, 522, 652, 810, 370, 627, 319, 630, 921]);
    sparkline('#sparkline-3', [15, 19, 20, 22, 33, 27, 31, 27, 19, 30, 21]);
  },

  // ---- Dashboard v2 (index2.html) ----------------------------------------

  // Monthly sales area chart.
  salesChart(el) {
    if (!el || typeof Chart === 'undefined') return;
    areaChart(el);
  },

  // Browser-usage donut chart.
  donut(el) {
    if (!el || typeof Chart === 'undefined') return;
    new Chart(el, {
      type: 'doughnut',
      data: {
        labels: ['Chrome', 'Edge', 'FireFox', 'Safari', 'Opera', 'IE'],
        datasets: [
          {
            label: 'Users',
            data: [700, 500, 400, 600, 300, 100],
            backgroundColor: ['#0d6efd', '#20c997', '#ffc107', '#d63384', '#6f42c1', '#adb5bd'],
          },
        ],
      },
      options: {
        cutout: '65%',
        plugins: { legend: { position: 'right' } },
      },
    });
  },

  // ---- Dashboard v3 (index3.html) ----------------------------------------

  // Online-store visitors line chart.
  visitorsChart(el) {
    if (!el || typeof Chart === 'undefined') return;
    new Chart(el, {
      type: 'line',
      data: {
        labels: ['22th', '23th', '24th', '25th', '26th', '27th', '28th'],
        datasets: [
          {
            label: 'High - 2023',
            data: [100, 120, 170, 167, 180, 177, 160],
            borderColor: '#0d6efd',
            backgroundColor: '#0d6efd',
            pointRadius: 3,
            tension: 0.4,
          },
          {
            label: 'Low - 2023',
            data: [60, 80, 70, 67, 80, 77, 100],
            borderColor: '#adb5bd',
            backgroundColor: '#adb5bd',
            pointRadius: 3,
            tension: 0.4,
          },
        ],
      },
      options: { plugins: { legend: { display: false } } },
    });
  },

  // Sales bar chart.
  salesBar(el) {
    if (!el || typeof Chart === 'undefined') return;
    new Chart(el, {
      type: 'bar',
      data: {
        labels: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
        datasets: [
          { label: 'Net Profit', data: [44, 55, 57, 56, 61, 58, 63, 60, 66], backgroundColor: '#0d6efd' },
          { label: 'Revenue', data: [76, 85, 101, 98, 87, 105, 91, 114, 94], backgroundColor: '#20c997' },
          { label: 'Free Cash Flow', data: [35, 41, 36, 26, 45, 48, 52, 53, 41], backgroundColor: '#ffc107' },
        ],
      },
      options: {
        datasets: { bar: { categoryPercentage: 0.55, barPercentage: 0.9 } },
        plugins: {
          legend: { display: false },
          tooltip: {
            callbacks: {
              label: (context) => ` ${context.dataset.label}: $ ${context.parsed.y} thousands`,
            },
          },
        },
      },
    });
  },

  // ---- Calendar (pages/calendar.html) ------------------------------------

  // FullCalendar dayGrid month with sample events + draggable external events.
  calendar(el, externalEl, removeAfterDropEl) {
    if (!el || typeof FullCalendar === 'undefined') return;

    // Keep all-day dates local; UTC conversion can shift them by a day.
    const isoDate = (d) =>
      [d.getFullYear(), String(d.getMonth() + 1).padStart(2, '0'), String(d.getDate()).padStart(2, '0')].join('-');
    const today = new Date();
    const offsetDay = (n) => {
      const d = new Date(today);
      d.setDate(today.getDate() + n);
      return isoDate(d);
    };

    if (externalEl) {
      new FullCalendar.Interaction.Draggable(externalEl, {
        itemSelector: '.draggable-event',
        eventData: (item) => ({
          title: item.textContent.trim(),
          color: item.dataset.color,
        }),
      });
    }

    const cellObservers = new WeakMap();
    const calendar = new FullCalendar.Calendar(el, {
      initialView: 'dayGridMonth',
      buttons: { today: { hint: 'Today' } },
      // Date cells also contain event titles; include that visible text in their
      // accessible names, including after drag, edit or navigation.
      dayCellDidMount: ({ el: cell }) => {
        const dateLabel = cell.getAttribute('aria-label') || '';
        const syncLabel = () => {
          const visibleText = cell.textContent.replace(/\s+/g, ' ').trim();
          cell.setAttribute('aria-label', `${dateLabel} ${visibleText}`.trim());
        };
        const observer = new MutationObserver(syncLabel);
        observer.observe(cell, { childList: true, subtree: true, characterData: true });
        cellObservers.set(cell, observer);
        syncLabel();
      },
      dayCellWillUnmount: ({ el: cell }) => {
        cellObservers.get(cell)?.disconnect();
        cellObservers.delete(cell);
      },
      headerToolbar: {
        start: 'prev,next today',
        center: 'title',
        end: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek',
      },
      height: 700,
      editable: true,
      droppable: true,
      dayMaxEvents: 3,
      drop: (info) => {
        if (removeAfterDropEl && removeAfterDropEl.checked) {
          info.draggedEl.remove();
        }
      },
      dateClick: (info) => {
        const title = prompt('Event title:');
        if (title) {
          calendar.addEvent({
            title: title,
            start: info.dateStr,
            allDay: info.allDay,
          });
        }
      },
      eventClick: (info) => {
        if (confirm(`Delete "${info.event.title}"?`)) {
          info.event.remove();
        }
      },
      events: [
        {
          title: 'Quarterly planning',
          start: offsetDay(-2),
          color: '#0d6efd',
        },
        {
          title: 'Onboarding session',
          start: offsetDay(1),
          color: '#198754',
        },
        {
          title: 'Design review',
          start: offsetDay(3),
          end: offsetDay(4),
          color: '#ffc107',
          contrastColor: '#000',
        },
        {
          title: 'Release v2.5',
          start: offsetDay(7),
          color: '#dc3545',
        },
        {
          title: 'All-hands',
          start: offsetDay(10),
          color: '#6f42c1',
        },
      ],
    });

    calendar.render();
  },

  // ---- Kanban (pages/kanban.html) ----------------------------------------

  // Run SortableJS on each lane element; a shared group lets cards move freely.
  kanban(lanes) {
    if (!lanes || typeof Sortable === 'undefined') return;
    lanes.forEach((el) => {
      if (!el) return;
      new Sortable(el, {
        group: 'kanban',
        animation: 150,
        ghostClass: 'sortable-ghost',
        dragClass: 'sortable-drag',
      });
    });
  },

  // ---- Rich-text editor (Quill) ------------------------------------------

  quill(el) {
    if (!el || typeof Quill === 'undefined') return;
    new Quill(el, {
      theme: 'snow',
      modules: {
        toolbar: [
          ['bold', 'italic', 'underline'],
          ['link', 'image'],
          [{ list: 'ordered' }, { list: 'bullet' }],
          ['clean'],
        ],
      },
    });
  },
};

// Shared "Digital Goods / Electronics" area chart used by Dashboard v1 and v2.
function areaChart(canvas) {
  const { areaFill } = window.lteChartTheme;
  new Chart(canvas, {
    type: 'line',
    data: {
      labels: ["Jan '23", "Feb '23", "Mar '23", "Apr '23", "May '23", "Jun '23", "Jul '23"],
      datasets: [
        {
          label: 'Digital Goods',
          data: [28, 48, 40, 19, 86, 27, 90],
          borderColor: '#0d6efd',
          backgroundColor: areaFill('#0d6efd'),
          pointBackgroundColor: '#0d6efd',
          fill: true,
          tension: 0.4,
        },
        {
          label: 'Electronics',
          data: [65, 59, 80, 81, 56, 55, 40],
          borderColor: '#20c997',
          backgroundColor: areaFill('#20c997'),
          pointBackgroundColor: '#20c997',
          fill: true,
          tension: 0.4,
        },
      ],
    },
    options: {
      plugins: {
        legend: { display: false },
        tooltip: {
          callbacks: {
            title: ([item]) =>
              `${new Date(2023, item.dataIndex).toLocaleString('en', { month: 'long' })} 2023`,
          },
        },
      },
      scales: { y: { beginAtZero: true } },
    },
  });
}
