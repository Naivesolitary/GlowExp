let financialChart = null;
let preferredCurrencyCode = "USD";

window.setCurrencyCode = (code) => {
    preferredCurrencyCode = code || "USD"
}
window.updatePieChart = (chartData) => {
    const ctx = document.getElementById('financialPieChart');

    if (!ctx) {
        console.error('Canvas element not found');
        return;
    }

    // Destroy existing chart if it exists
    if (financialChart) {
        financialChart.destroy();
    }

    // Create new chart
    financialChart = new Chart(ctx, {
        type: 'pie',
        data: chartData,
        options: {
            responsive: true,
            maintainAspectRatio: true,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        padding: 20,
                        font: {
                            size: 14
                        }
                    }
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const label = context.label || '';
                            const value = context.parsed;
                            const total = context.dataset.data.reduce((a, b) => a + b, 0);
                            const percentage = total > 0 ? ((value / total) * 100).toFixed(1) : '0.0';
                            return `${label}: ${preferredCurrencyCode} ${value.toFixed(2)} (${percentage}%)`;
                        }
                    }
                }
            },
            animation: {
                duration: 1000,
                easing: 'easeOutQuart'
            }
        }
    });
};