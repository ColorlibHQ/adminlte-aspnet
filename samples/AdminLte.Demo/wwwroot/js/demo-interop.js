// AdminLTE demo plugin interop for the Blazor (InteractiveServer) sample.
// Each function initialises a third-party plugin against a DOM element passed
// from a Blazor page (via @ref ElementReference) in OnAfterRenderAsync.
// The ApexCharts option objects are copied verbatim from the core AdminLTE
// dist demo pages (index2.html / index3.html).
window.adminlteDemo = {
  // ---- Dashboard v1 (index.html) -----------------------------------------

  // Sales Value area chart (copied verbatim from the core dist/index.html demo).
  salesAreaChart(el) {
    if (!el) return;
    const sales_chart_options = {
      series: [
        {
          name: 'Digital Goods',
          data: [28, 48, 40, 19, 86, 27, 90],
        },
        {
          name: 'Electronics',
          data: [65, 59, 80, 81, 56, 55, 40],
        },
      ],
      chart: {
        height: 300,
        type: 'area',
        toolbar: {
          show: false,
        },
      },
      legend: {
        show: false,
      },
      colors: ['#0d6efd', '#20c997'],
      dataLabels: {
        enabled: false,
      },
      stroke: {
        curve: 'smooth',
      },
      xaxis: {
        type: 'datetime',
        categories: [
          '2023-01-01',
          '2023-02-01',
          '2023-03-01',
          '2023-04-01',
          '2023-05-01',
          '2023-06-01',
          '2023-07-01',
        ],
      },
      tooltip: {
        x: {
          format: 'MMMM yyyy',
        },
      },
    };
    new ApexCharts(el, sales_chart_options).render();
  },

  // World map (jsvectormap) + the three footer sparklines, copied verbatim from
  // the core dist/index.html demo. The sparklines target the #sparkline-1/2/3
  // ids rendered alongside the map card.
  worldMap(el) {
    if (!el || typeof jsVectorMap === 'undefined') return;

    // World map by jsVectorMap
    new jsVectorMap({
      selector: el,
      map: 'world',
    });

    // Sparkline charts
    const option_sparkline1 = {
      series: [
        {
          data: [1000, 1200, 920, 927, 931, 1027, 819, 930, 1021],
        },
      ],
      chart: {
        type: 'area',
        height: 50,
        sparkline: {
          enabled: true,
        },
      },
      stroke: {
        curve: 'straight',
      },
      fill: {
        opacity: 0.3,
      },
      yaxis: {
        min: 0,
      },
      colors: ['#DCE6EC'],
    };

    const sparkline1El = document.querySelector('#sparkline-1');
    if (sparkline1El) new ApexCharts(sparkline1El, option_sparkline1).render();

    const option_sparkline2 = {
      series: [
        {
          data: [515, 519, 520, 522, 652, 810, 370, 627, 319, 630, 921],
        },
      ],
      chart: {
        type: 'area',
        height: 50,
        sparkline: {
          enabled: true,
        },
      },
      stroke: {
        curve: 'straight',
      },
      fill: {
        opacity: 0.3,
      },
      yaxis: {
        min: 0,
      },
      colors: ['#DCE6EC'],
    };

    const sparkline2El = document.querySelector('#sparkline-2');
    if (sparkline2El) new ApexCharts(sparkline2El, option_sparkline2).render();

    const option_sparkline3 = {
      series: [
        {
          data: [15, 19, 20, 22, 33, 27, 31, 27, 19, 30, 21],
        },
      ],
      chart: {
        type: 'area',
        height: 50,
        sparkline: {
          enabled: true,
        },
      },
      stroke: {
        curve: 'straight',
      },
      fill: {
        opacity: 0.3,
      },
      yaxis: {
        min: 0,
      },
      colors: ['#DCE6EC'],
    };

    const sparkline3El = document.querySelector('#sparkline-3');
    if (sparkline3El) new ApexCharts(sparkline3El, option_sparkline3).render();
  },

  // ---- Dashboard v2 (index2.html) ----------------------------------------

  // Monthly sales area chart.
  salesChart(el) {
    if (!el) return;
    const sales_chart_options = {
      series: [
        {
          name: 'Digital Goods',
          data: [28, 48, 40, 19, 86, 27, 90],
        },
        {
          name: 'Electronics',
          data: [65, 59, 80, 81, 56, 55, 40],
        },
      ],
      chart: {
        height: 180,
        type: 'area',
        toolbar: {
          show: false,
        },
      },
      legend: {
        show: false,
      },
      colors: ['#0d6efd', '#20c997'],
      dataLabels: {
        enabled: false,
      },
      stroke: {
        curve: 'smooth',
      },
      xaxis: {
        type: 'datetime',
        categories: [
          '2023-01-01',
          '2023-02-01',
          '2023-03-01',
          '2023-04-01',
          '2023-05-01',
          '2023-06-01',
          '2023-07-01',
        ],
      },
      tooltip: {
        x: {
          format: 'MMMM yyyy',
        },
      },
    };
    new ApexCharts(el, sales_chart_options).render();
  },

  // Browser-usage donut chart.
  donut(el) {
    if (!el) return;
    const pie_chart_options = {
      series: [700, 500, 400, 600, 300, 100],
      chart: {
        type: 'donut',
        // Pin an explicit height to avoid an ApexCharts ResizeObserver feedback
        // loop on browser zoom (most visible on Edge). Fixes #6019.
        height: 350,
      },
      labels: ['Chrome', 'Edge', 'FireFox', 'Safari', 'Opera', 'IE'],
      dataLabels: {
        enabled: false,
      },
      colors: ['#0d6efd', '#20c997', '#ffc107', '#d63384', '#6f42c1', '#adb5bd'],
    };
    new ApexCharts(el, pie_chart_options).render();
  },

  // ---- Dashboard v3 (index3.html) ----------------------------------------

  // Online-store visitors line chart.
  visitorsChart(el) {
    if (!el) return;
    const visitors_chart_options = {
      series: [
        {
          name: 'High - 2023',
          data: [100, 120, 170, 167, 180, 177, 160],
        },
        {
          name: 'Low - 2023',
          data: [60, 80, 70, 67, 80, 77, 100],
        },
      ],
      chart: {
        height: 200,
        type: 'line',
        toolbar: {
          show: false,
        },
      },
      colors: ['#0d6efd', '#adb5bd'],
      stroke: {
        curve: 'smooth',
      },
      grid: {
        borderColor: '#e7e7e7',
        row: {
          colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
          opacity: 0.5,
        },
      },
      legend: {
        show: false,
      },
      markers: {
        size: 1,
      },
      xaxis: {
        categories: ['22th', '23th', '24th', '25th', '26th', '27th', '28th'],
      },
    };
    new ApexCharts(el, visitors_chart_options).render();
  },

  // Sales bar chart.
  salesBar(el) {
    if (!el) return;
    const sales_chart_options = {
      series: [
        {
          name: 'Net Profit',
          data: [44, 55, 57, 56, 61, 58, 63, 60, 66],
        },
        {
          name: 'Revenue',
          data: [76, 85, 101, 98, 87, 105, 91, 114, 94],
        },
        {
          name: 'Free Cash Flow',
          data: [35, 41, 36, 26, 45, 48, 52, 53, 41],
        },
      ],
      chart: {
        type: 'bar',
        height: 200,
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: '55%',
          endingShape: 'rounded',
        },
      },
      legend: {
        show: false,
      },
      colors: ['#0d6efd', '#20c997', '#ffc107'],
      dataLabels: {
        enabled: false,
      },
      stroke: {
        show: true,
        width: 2,
        colors: ['transparent'],
      },
      xaxis: {
        categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
      },
      fill: {
        opacity: 1,
      },
      tooltip: {
        y: {
          formatter: function (val) {
            return '$ ' + val + ' thousands';
          },
        },
      },
    };
    new ApexCharts(el, sales_chart_options).render();
  },

  // ---- Calendar (pages/calendar.html) ------------------------------------

  // FullCalendar dayGrid month with sample events + draggable external events.
  calendar(el, externalEl, removeAfterDropEl) {
    if (!el || typeof FullCalendar === 'undefined') return;

    const isoDate = (d) => d.toISOString().slice(0, 10);
    const today = new Date();
    const offsetDay = (n) => {
      const d = new Date(today);
      d.setDate(today.getDate() + n);
      return isoDate(d);
    };

    if (externalEl) {
      new FullCalendar.Draggable(externalEl, {
        itemSelector: '.draggable-event',
        eventData: (item) => ({
          title: item.textContent.trim(),
          backgroundColor: item.dataset.color,
          borderColor: item.dataset.color,
        }),
      });
    }

    const calendar = new FullCalendar.Calendar(el, {
      initialView: 'dayGridMonth',
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
          backgroundColor: '#0d6efd',
          borderColor: '#0d6efd',
        },
        {
          title: 'Onboarding session',
          start: offsetDay(1),
          backgroundColor: '#198754',
          borderColor: '#198754',
        },
        {
          title: 'Design review',
          start: offsetDay(3),
          end: offsetDay(4),
          backgroundColor: '#ffc107',
          borderColor: '#ffc107',
          textColor: '#000',
        },
        {
          title: 'Release v2.5',
          start: offsetDay(7),
          backgroundColor: '#dc3545',
          borderColor: '#dc3545',
        },
        {
          title: 'All-hands',
          start: offsetDay(10),
          backgroundColor: '#6f42c1',
          borderColor: '#6f42c1',
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
